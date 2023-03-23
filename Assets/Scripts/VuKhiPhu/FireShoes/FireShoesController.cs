using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuKhi;

public class FireShoesController : Base
{
    [SerializeField]
    private GameObject fireShoes;

    public static bool maxShoes = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Core.PlayerController>();
        var firecheck = other.gameObject.GetComponent<FireOfShoesControl>();
        if (player != null && firecheck == null)
        {
            other.gameObject.AddComponent<FireOfShoesControl>();
            other.gameObject.GetComponent<FireOfShoesControl>().fireshoes = this.fireShoes;
			Destroy(gameObject);
		}
        else if (player != null && firecheck != null)
        {
            if (firecheck.count <= 5)
            {
                firecheck.liftime += 2;
                firecheck.count++;
			}
            else
            {
				player.Heal(20);
			}
			Destroy(gameObject);
		}
	}
}
