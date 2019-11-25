using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Values : MonoBehaviour
{
    // TODO: Fix class incapsulation

    public static Structure[] structure = new Structure[3];
    public static Tripwire tripwire;
    public static bool buildingRuined;
    [SerializeField]
    private GameObject[] toStructure;
    [SerializeField]
    private GameObject[] toTripwire;

    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private Text canText;
    [SerializeField]
    private Text ropeText;
    [SerializeField]
    private Text stickText;
    [SerializeField]
    private GameObject inventory;

    private static GameObject fettHealthBar;
    private Text healthBar;
    private Animator invAnim;

    private static AudioSource audios;
    private static AudioClip staticInjureSound;
    [SerializeField]
    private AudioClip FettInjureSound;

    private static int playerHealth;
    private static int fettHealth;
    private static float revolvAmmo;
    private static float trapAmmo;
    private static float canCount;
    private static float ropeCount;
    private static float stickCount;

    public static float revolvAmmoOnLoad = 6;
    public static float trapAmmoOnLoad = 1;

    void Awake()
    {
        for (int i = 0; i < structure.Length; i++)
            structure[i] = new Structure(toStructure[i], toStructure[i].name);
    }

    void Start()
    {
        audios = GameObject.FindWithTag("Enemy").GetComponent<AudioSource>();
        staticInjureSound = FettInjureSound;
        fettHealthBar = GameObject.FindWithTag("FettBar");
        healthBar = GameObject.FindWithTag("PlayerBar").GetComponent<Text>();
        invAnim = inventory.GetComponent<Animator>();

        tripwire = new Tripwire(toTripwire);
        playerHealth = 100;
        fettHealth = 200;
        ResetInventory();
    }

    public void FixedUpdate()
    {
        healthBar.text = playerHealth + " ♥";
        canText.text = canCount.ToString();
        ropeText.text = ropeCount.ToString();
        stickText.text = stickCount.ToString();

        // Game Over
        if (fettHealth <= 0 || playerHealth <= 0 || buildingRuined)
        {
            loseScreen.SetActive(true);
            Destroy(GameObject.FindWithTag("Enemy"));
            StartCoroutine(Cooldown());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            invAnim.SetBool("isOpen", true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            invAnim.SetBool("isOpen", false);
    }

    // TODO: Add save/load support
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5f);
        ResetInventory();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void ResetInventory()
    {
        revolvAmmo = revolvAmmoOnLoad;
        trapAmmo = trapAmmoOnLoad;
        canCount = 0;
        ropeCount = 0;
        stickCount = 0;
        buildingRuined = false;
    }

    // Setters
    public static void ChangeFettHealth(int amount)
    {
        audios.clip = staticInjureSound;
        audios.Play();
        fettHealth = Mathf.Clamp(fettHealth + amount, 0, 200);
        fettHealthBar.transform.Translate(new Vector2(amount * 1.465f, 0));
    }

    public static void ChangePlayerHealth(int amount)
    {
        playerHealth = Mathf.Clamp(playerHealth + amount, 0, 100);
    }

    public static void AddAmmo(float amount)
    {
        revolvAmmo += amount;
    }

    public static void AddTrap(float amount)
    {
        trapAmmo += amount;
    }

    public static void AddAmmo()
    {
        revolvAmmo += 3;
    }

    public static void AddTrap()
    {
        trapAmmo++;
    }

    public static void AddCan()
    {
        canCount++;
    }

    public static void AddRope()
    {
        ropeCount++;
    }

    public static void AddStick()
    {
        stickCount++;
    }

    public static void DecreaseItems()
    {
        canCount--;
        ropeCount--;
        stickCount -= 2;
    }

    // Getters
    public static int GetPlayerHealth()
    {
        return playerHealth;
    }

    public static int GetFettHealth()
    {
        return fettHealth;
    }

    public static float GetRevolvAmmo()
    {
        return revolvAmmo;
    }

    public static float GetTrapAmmo()
    {
        return trapAmmo;
    }

    public static float GetCanCount()
    {
        return canCount;
    }

    public static float GetRopeCount()
    {
        return ropeCount;
    }

    public static float GetStickCount()
    {
        return stickCount;
    }

    public static bool CanPlaceTrap()
    {
        if (canCount > 0 && ropeCount > 0 && stickCount > 1)
            return true;
        else
            return false;
    }
}

public class Structure
{
    public GameObject buildingObject;
    public int buildingHealth = 500;
    public string displayName;

    public Structure(GameObject buildingObject, string displayName)
    {
        this.buildingObject = buildingObject;
        this.displayName = displayName;
    }

    public void ChangeBuildingHealth(int amount)
    {
        buildingHealth = Mathf.Clamp(buildingHealth + amount, 0, 500);
        if (buildingHealth <= 0)
            Values.buildingRuined = true;
    }

    public int GetBuildingHealth()
    {
        return buildingHealth;
    }

    public GameObject GetLabel()
    {
        return buildingObject.GetComponentInChildren<SpriteRenderer>().gameObject;
    }
}

public class Tripwire
{
    public GameObject[] _tripwires;

    public Tripwire(GameObject[] _tripwires)
    {
        this._tripwires = _tripwires;
    }

    public void Place(GameObject template)
    {
        template.SetActive(false);
        _tripwires[int.Parse(template.name)].SetActive(true);
        Values.DecreaseItems();
    }
}