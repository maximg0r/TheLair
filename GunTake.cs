using UnityEngine;
using System.Collections;

public class GunTake : MonoBehaviour
{

    [SerializeField] private GameObject[] weapon;
    [SerializeField] private GameObject revolvHint;
    [SerializeField] private GameObject loseScreen;
    private Animator revolvAnim;
    private Animator trapAnim;
    public static int currentWeapon = 0;

    void Start()
    {
        revolvAnim = weapon[1].GetComponent<Animator>();
        trapAnim = weapon[2].GetComponent<Animator>();
    }

    void Update()
    {
        if (loseScreen.activeSelf)
            currentWeapon = 0;
        if (Input.GetKeyUp("1"))
            RevolvTake();
        if (Input.GetKeyUp("2"))
            TrapTake();
    }

    void RevolvTake()
    {
        switch (revolvAnim.GetBool("InHand"))
        {
            case false:
                if (currentWeapon != 0)
                    weapon[currentWeapon].GetComponent<Animator>().SetBool("InHand", false);
                currentWeapon = 1;
                weapon[currentWeapon].SetActive(true);
                revolvAnim.SetBool("InHand", true);
                if (revolvHint.activeSelf)
                    revolvHint.SetActive(false);
                break;

            case true:
                revolvAnim.SetBool("InHand", false);
                currentWeapon = 0;
                break;
        }
    }

    void TrapTake()
    {
        switch (trapAnim.GetBool("InHand"))
        {
            case false:
                if (currentWeapon != 0)
                    weapon[currentWeapon].GetComponent<Animator>().SetBool("InHand", false);
                currentWeapon = 2;
                weapon[currentWeapon].SetActive(true);
                trapAnim.SetBool("InHand", true);
                break;

            case true:
                trapAnim.SetBool("InHand", false);
                currentWeapon = 0;
                break;
        }
    }
}
