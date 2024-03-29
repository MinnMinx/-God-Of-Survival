using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class MapController : MonoBehaviour
    {
        [SerializeField]
        private Material mapMaterial;
        [SerializeField]
        private Vector2 uvScale = Vector2.one;

        private MeshRenderer _renderer;
        private MeshFilter _meshHolder;
        private MaterialPropertyBlock materialBlock;

        // Start is called before the first frame update
        void Start()
        {
            if (SceneDataKeeper.Singleton != null)
			{
				mapMaterial.mainTexture = SceneDataKeeper.Singleton.mapChoice;
				uvScale = SceneDataKeeper.Singleton.mapScale;
			}
            Refresh();
		}

        public void Move(float offsetX, float offsetY)
        {
            if (materialBlock == null || _renderer == null)
            {
                Refresh();
            }
            Vector4 textureVector = materialBlock.GetVector("_MainTex_ST");

            // is using orthographic camera, Vertices will always be placed on (2x, 2y)
            textureVector.z = offsetX / 2f * uvScale.x;    // offset.x will always be in range 0-1
            textureVector.w = offsetY / 2f * uvScale.y;    // offset.y will always be in range 0-1
            materialBlock.SetVector("_MainTex_ST", textureVector);
            _renderer.SetPropertyBlock(materialBlock);

            // set transform
            var position = transform.position;
            position.x = offsetX;
            position.y = offsetY;
            transform.position = position;
        }

        void Refresh()
        {
            if (_meshHolder == null)
            {
                _meshHolder = GetComponent<MeshFilter>();
            }
            if (_renderer == null)
            {
                _renderer = GetComponent<MeshRenderer>();
            }
            _meshHolder.mesh = CreateScreenMesh();
            _renderer.material = mapMaterial;

            materialBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(materialBlock);
            materialBlock.SetVector("_MainTex_ST", new Vector4(1, 1, 0, 0));
        }

        Mesh CreateScreenMesh()
        {
            Mesh mesh = new Mesh();

            Vector2 worldScreenSize = Camera.main.ViewportToWorldPoint(Vector2.one);

            // set vervices
            // is using orthographic camera, so ViewportToWorldPoint(1, 1)
            // will always return half of screen size
            mesh.SetVertices(new Vector3[]
            {
            new Vector3(-worldScreenSize.x, worldScreenSize.y),   // top left
            new Vector3(worldScreenSize.x, worldScreenSize.y),    // top right
            new Vector3(-worldScreenSize.x, -worldScreenSize.y),  // bottom left
            new Vector3(worldScreenSize.x, -worldScreenSize.y)    // bottom right
            });

            // set triangles
            mesh.triangles = new int[]
            {
            0, 1, 2,
            2, 1, 3,
            };

            // set uv
            mesh.uv = new Vector2[]
            {
            new Vector2(0, worldScreenSize.y * uvScale.y),      // top left
            worldScreenSize * uvScale,                          // top right
            Vector2.zero,                                       // bottom left
            new Vector2(worldScreenSize.x * uvScale.x, 0),      // bottom right
            };

            return mesh;
        }
    }
}