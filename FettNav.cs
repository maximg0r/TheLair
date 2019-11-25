using UnityEngine;
using System.Collections;

public class FettNav : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerDoll;
    private GameObject Player;
    private GameObject Fett;

    [SerializeField]
    private AudioClip punchSound;
    [SerializeField]
    private AudioClip crashSound;
    [SerializeField]
    private AudioSource runSound;
    [SerializeField]
    private AudioClip screamerSound;

    private AudioSource audios;
    private Animator anim;
    private NavMeshAgent agent;

    public Transform[] points;
    private float vision;
    private int destPoint = 0;
    public static bool chasing = false;
    public static bool isAttacking = false;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Fett = GameObject.FindWithTag("Enemy");

        audios = Fett.GetComponent<AudioSource>();
        anim = Fett.GetComponent<Animator>();
        agent = Fett.GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        StartCoroutine(ScreamerSound());
    }

    void Update()
    {
        //  Debug.Log(Vector3.Distance(Fett.transform.position, points[destPoint].position));

        if (chasing)
            Chase();

        if (!anim.GetBool("Run"))
            Patrol();

        // Player crouching/running?
        if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.m_IsWalking)
            vision = 35f;
        else
            vision = 45f;

        // Stop running
        if (anim.GetBool("Run") && !chasing)
        {
            if (Vector3.Distance(Fett.transform.position, points[destPoint].position) < 3f)
                anim.SetBool("Run", false);

            for (int i = 0; i < Values.structure.Length; i++)
                if (isAttacking && Vector3.Distance(Fett.transform.position, Values.structure[i].buildingObject.transform.position) < 5f)
                {
                    audios.clip = crashSound;
                    audios.Play();
                    Values.structure[i].ChangeBuildingHealth(-100);
                    agent.SetDestination(points[++destPoint].position);
                    isAttacking = false;
                }
        }

        // Running sound
        if (!anim.GetBool("Run"))
            runSound.Stop();
        else if (!runSound.isPlaying)
            runSound.Play();
        
    }

    void Patrol()
    {
        agent.SetDestination(points[destPoint].position);
        if (Vector3.Distance(Fett.transform.position, points[destPoint].position) < 3f)
        {
            Attack(destPoint);
            destPoint++;
        }
        if (destPoint == points.Length)
            destPoint = 0;

        // Chasing detect
        Collider[] hitColliders = Physics.OverlapSphere(Fett.transform.position, vision);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Player")
            {
                chasing = true;
                break;
            }
            i++;
        }
    }

    void Chase()
    {
        anim.SetBool("Run", true);
        agent.SetDestination(Player.transform.position);
        if (Vector3.Distance(Fett.transform.position, Player.transform.position) < 2f)
        {
            audios.clip = punchSound;
            audios.Play();
            Values.ChangePlayerHealth(-20);
            chasing = false;
            agent.SetDestination(points[destPoint].position);
        }
    }

    void Attack(int currentPoint)
    {
        int rand = Random.Range(0, 3);
        Debug.Log("Rand: " + rand);
        if (rand == 2)
            switch (currentPoint)
            {
                case 2:
                    agent.SetDestination(Values.structure[0].buildingObject.transform.position);
                    anim.SetBool("Run", true);
                    isAttacking = true;
                    break;
                case 3:
                    agent.SetDestination(Values.structure[1].buildingObject.transform.position);
                    anim.SetBool("Run", true);
                    isAttacking = true;
                    break;
                case 5:
                    agent.SetDestination(Values.structure[2].buildingObject.transform.position);
                    anim.SetBool("Run", true);
                    isAttacking = true;
                    break;
            }
    }

    private IEnumerator ScreamerSound()
    {
        yield return new WaitForSeconds(Random.Range(240f, 360f));
        audios.clip = screamerSound;
        audios.Play();
    }
}