using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject AmmoBar;
    [SerializeField]
    private GameObject TrapPrefub;
    private GameObject Player;

    private Animator anim;
    private Text AmmoCount;
    [SerializeField]
    private Transform Revolver;

    [SerializeField]
    private int revolverDamage;
    [SerializeField]
    private float revolverDistance;
    private static bool _cooldown = false;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        anim = GameObject.Find("Ethan").GetComponent<Animator>();
        AmmoCount = AmmoBar.GetComponent<Text>();
    }

    void Update()
    {
        //	Debug.DrawRay (Camera.main.transform.position + transform.forward * 0.2f, Camera.main.transform.forward * revolverDistance);
        
        // Shoot
        if (Input.GetButtonUp("Fire1") && !_cooldown && Time.timeScale != 0)
            Shoot(GunTake.currentWeapon);

        // Ammo bar
        if (GunTake.currentWeapon == 0)
            AmmoBar.SetActive(false);
        else
        {
            AmmoBar.SetActive(true);
            if (GunTake.currentWeapon == 1)
                AmmoCount.text = Values.GetRevolvAmmo().ToString();
            else if (GunTake.currentWeapon == 2)
                AmmoCount.text = Values.GetTrapAmmo().ToString();
        }
    }

    void OnAnimatorIK()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, Revolver.position - Revolver.forward * 0.12f + Revolver.right * 0.05f - Revolver.up * 0.08f);
    }

    void Shoot(int weapon)
    {
        switch (weapon)
        {
            case 1:
                if (Values.GetRevolvAmmo() > 0)
                {
                    Ray ray = new Ray(Camera.main.transform.position + transform.forward * 0.2f, Camera.main.transform.forward);
                    RaycastHit hit;
                    Revolver.gameObject.GetComponent<AudioSource>().Play();
                    if (Physics.Raycast(ray, out hit) && hit.distance < revolverDistance && hit.collider.gameObject.tag == "Enemy")
                    {
                        //		Debug.Log ("Distance: " + hit.distance + " GameObject " + hit.collider.gameObject);
                        Values.ChangeFettHealth(revolverDamage);
                    }
                    Values.AddAmmo(-1);
                    StartCoroutine(Cooldown());
                }
                break;
            case 2:
                if (Values.GetTrapAmmo() > 0)
                {
                    Instantiate(TrapPrefub, Player.transform.position + Player.transform.forward, Quaternion.identity);
                    Values.AddTrap(-1);
                    StartCoroutine(Cooldown());
                }
                break;
        }
    }

    private IEnumerator Cooldown()
    {
        _cooldown = true;
        yield return new WaitForSeconds(1.5f);
        _cooldown = false;
    }
}
