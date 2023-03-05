using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
	private GameObject prefab;

    [SerializeField]
    private PositionController positionController;

    // Start is called before the first frame update
    void Start()
    {
        // create object from prefab
        var gameObj = Instantiate(prefab);

        // add to controller
        positionController.RegisterComponent(gameObj.GetComponent<PositionComponent>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
