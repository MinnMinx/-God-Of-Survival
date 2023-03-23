using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataKeeper : MonoBehaviour
{
    private static SceneDataKeeper _singleton;
    public static SceneDataKeeper Singleton => _singleton;

    // Start is called before the first frame update
    void Awake()
	{
		DontDestroyOnLoad(this);
		if (_singleton == null)
        {
            _singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int WeaponChoice { get; set; }
    public Texture2D mapChoice { get; set; }
	public Vector2 mapScale { get; set; }
	public RuntimeAnimatorController characterAnimator { get; set; }
}
