using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    private PositionComponent component;

    // Start is called before the first frame update
    void Start()
    {
        // Get position component for modifying
        component = GetComponent<PositionComponent>();
    }

    bool forward = false;
    // Update is called once per frame
    void Update()
    {
        // moving position component
        if (component != null)
        {
            if (forward && component.Position.x > 5f)
            {
                forward = false;
			}
            else if (!forward && component.Position.x < -5f)
			{
                forward = true;
			}
			var position = component.Position;
			position.x += Time.deltaTime * 2 * (forward ? 1f : -1f);
			component.Position = position;
		}
    }
}
