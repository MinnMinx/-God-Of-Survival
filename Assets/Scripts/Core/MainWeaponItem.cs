using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeaponItem : MonoBehaviour
{
    private Action onPickUp;
    public void SetPickUp(Action onPickUp = null)
    {
        this.onPickUp = onPickUp;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.GetComponent<Core.PlayerController>() != null)
        {
            onPickUp();
            Destroy(gameObject);
        }
	}
}
