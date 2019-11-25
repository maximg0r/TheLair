using UnityEngine;
using System.Collections;

public class TrapCollision : MonoBehaviour
{
    private int trapDamage = -50;
    private AudioSource trigger;

    void Start()
    {
        trigger = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Values.ChangePlayerHealth(trapDamage + 10);
            trigger.Play();
        }
        else if (col.gameObject.tag == "Enemy")
        {
            Values.ChangeFettHealth(trapDamage);
            trigger.Play();
        }
    }
}
