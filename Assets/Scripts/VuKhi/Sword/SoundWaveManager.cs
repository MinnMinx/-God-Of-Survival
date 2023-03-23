using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject wavePrefab;
    [SerializeField]
    private Vector3 localSpawnPoint;

    public void Slash(float angle, float damage)
    {
        var gameObj = Instantiate(wavePrefab, transform.TransformPoint(localSpawnPoint), Quaternion.Euler(0, 0, angle));
        gameObj.GetComponent<SoundWaveBehavior>().SetAngle(angle, damage);
    }
}
