using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject flashlight;
    [SerializeField]
    private Light spot;

    [SerializeField]
    private AudioClip ButtonSound;
    private AudioSource audios;

    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    void Update()
    {
        flashlight.transform.rotation = Player.transform.rotation;
        if (Input.GetKeyUp("f"))
        {
            audios.clip = ButtonSound;
            audios.Play();
            spot.enabled = !spot.enabled;

        }
    }
}
