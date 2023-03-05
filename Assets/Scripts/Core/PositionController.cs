using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
	[SerializeField]
    private List<PositionComponent> positionComponents;
    private Vector2 playerOffset;

    [SerializeField]
    private MapController mapCtrl;
	[SerializeField]
	private float moveSpeed = 1f;
	// Start is called before the first frame update
	void Start()
    {
		if (positionComponents == null)
			positionComponents = new List<PositionComponent>();
	}

    // Update is called once per frame
    void Update()
    {
		TestKeyboardInput();

	}
    void TestKeyboardInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerOffset.x -= moveSpeed * Time.deltaTime;
		}
        else if (Input.GetKey(KeyCode.RightArrow))
		{
			playerOffset.x += moveSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			playerOffset.y -= moveSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.UpArrow))
		{
			playerOffset.y += moveSpeed * Time.deltaTime;
		}
	}

    // Update position of all registered component
	private void LateUpdate()
	{
		// Update components
		for(int i = 0; i < positionComponents.Count; i++)
		{
			UpdateComponent(positionComponents[i]);
		}

		// Update map position
		if (mapCtrl != null)
		{
			mapCtrl.SetPosition(playerOffset);
		}
	}

	public void RegisterComponent(PositionComponent newComponent)
    {
		if (!positionComponents.Contains(newComponent))
        {
            positionComponents.Add(newComponent);
			UpdateComponent(newComponent);
		}
	}

    void UpdateComponent(PositionComponent component)
    {
        component._internal_UpdatePosition(playerOffset);
    }
}
