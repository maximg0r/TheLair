using UnityEngine;
using System.Collections;

public class CheatMode : MonoBehaviour
{
    [SerializeField]
    private AudioClip PovarSound;
    private AudioSource audios;

    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyUp("p") && Input.GetKeyUp("o"))
        {
            audios.clip = PovarSound;
            audios.Play();
            Debug.Log("Cheat Mode On");
        }
    }
}
