using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionComponent : MonoBehaviour
{
    /// <summary>
    /// Position of object on the map
    /// </summary>
    public Vector2 Position { get; set; }

	/// <summary>
	/// Vector3 version of position of object on the map.
	/// (z is always = 0)
	/// </summary>
	public Vector3 Position3
	{
		get => new Vector3(Position.x, Position.y, 0);
		set
		{
			this.Position = value;
		}
	}

	/// <summary>
	/// DON'T USE THIS. For this will be update by PositionController
	/// </summary>
	/// <param name="playerOffset">For input player's offset</param>
	public void _internal_UpdatePosition(Vector2 playerOffset)
    {
        transform.position = Position - playerOffset;
    }

	private void Awake()
	{
		Position = transform.position;
        transform.position = Vector3.zero;
	}
}
