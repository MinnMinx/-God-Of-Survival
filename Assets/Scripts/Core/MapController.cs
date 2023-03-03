using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Refresh();
	}

    public void Move(float x, float y)
    {
        if (materialBlock == null || _renderer == null)
        {
            Refresh();
        }
		Vector4 textureVector = materialBlock.GetVector("_MainTex_ST");
        textureVector.z = (textureVector.z + x) % 1;    // offset.x will always be in range 0-1
        textureVector.w = (textureVector.w + y) % 1;    // offset.y will always be in range 0-1
		materialBlock.SetVector("_MainTex_ST", textureVector);
		_renderer.SetPropertyBlock(materialBlock);
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
            new Vector2(0, 0),                                  // bottom left
            new Vector2(worldScreenSize.x * uvScale.x, 0),      // bottom right
        };
        return mesh;
    }
}
