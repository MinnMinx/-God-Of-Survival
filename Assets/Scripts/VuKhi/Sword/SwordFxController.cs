using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFxController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem parent;

    public void Swing()
    {
        parent.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        parent.Play(true);
    }
}
