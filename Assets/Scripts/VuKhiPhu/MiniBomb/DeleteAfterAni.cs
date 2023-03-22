using UnityEngine;

public class DeleteAfterAni : MonoBehaviour
{
    private float ATK;
    // Use this for initialization
    void Start()
    {
        ATK = GameObject.Find("BombController").GetComponent<MiniBomb>().ATKBase;
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var monster = other.gameObject.GetComponent<Monster.Monster>();
        if (monster != null)
        {
            monster.takedamage(ATK);
        }
    }
}