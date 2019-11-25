using UnityEngine;
using System.Collections;

public class WireCollision : MonoBehaviour
{
    private int wireDamage = -75;
    private AudioSource explode;

    void Start()
    {
        explode = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Values.ChangePlayerHealth(wireDamage + 15);
            explode.Play();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Enemy")
        {
            Values.ChangeFettHealth(wireDamage);
            explode.Play();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
