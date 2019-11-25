using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject Message;
    [SerializeField]
    private GameObject BookMessage;
    [SerializeField]
    private GameObject BuildingMessage;
    [SerializeField]
    private GameObject Notebook;
    [SerializeField]
    private GameObject NoteTab;
    [SerializeField]
    private GameObject GuideTab;
    [SerializeField]
    private Image NoteBtn;
    [SerializeField]
    private Image GuideBtn;

    private bool bookWatched = false;
    [SerializeField]
    private GameObject[] NoteButtons;
    [SerializeField]
    private GameObject[] SavedNotes;
    [SerializeField]
    private GameObject[] SavedGuides;

    private string[] FoundNotes;
    private int numberOfNotes = 0;
    private int currentNote;
    private int currentGuide;

    [SerializeField]
    private AudioClip BookSound;
    private AudioSource audios;

    void Start()
    {
        FoundNotes = new string[GameObject.FindGameObjectsWithTag("Note").Length];
        audios = GameObject.FindWithTag("Respawn").GetComponent<AudioSource>();
    }

    public void FixedUpdate()
    {
        // Open notebook hint
        if (!bookWatched && FoundNotes[0] != null)
            BookMessage.SetActive(true);

        Message.SetActive(false);
        BuildingMessage.SetActive(false);
    }

    void Update()
    {
        //  Debug.DrawRay(Camera.main.transform.position + transform.forward * 0.2f, Camera.main.transform.forward);

        // Notebook UI setup
        if (Notebook.activeSelf)
            NotebookSetup();

        // Notebook toggle
        if (Input.GetKeyUp("b") && Time.timeScale != 0 && !Notebook.activeSelf)
            OpenNotebook();
        else if (Input.GetKeyUp("b") && Notebook.activeSelf)
            ExitNotebook();
    }

    private void NotebookSetup()
    {
        foreach (GameObject item in SavedNotes)
            item.SetActive(false);
        foreach (GameObject item in SavedGuides)
            item.SetActive(false);
        if (NoteTab.activeSelf && FoundNotes[currentNote] != null)
            SavedNotes[(int.Parse(FoundNotes[currentNote])) - 1].SetActive(true);
        if (GuideTab.activeSelf)
            SavedGuides[currentGuide].SetActive(true);
    }

    public void SetTab(bool tab)
    // True => Guides; False => Notes
    {
        if (tab)
        {
            GuideTab.SetActive(true);
            NoteTab.SetActive(false);
        }
        else
        {
            NoteTab.SetActive(true);
            GuideTab.SetActive(false);
        }
    }

    public void ChangeNote(int num)
    {
        currentNote = num;
    }

    public void ChangeGuide(int num)
    {
        currentGuide = num;
    }

    public void ExitNotebook()
    {
        Notebook.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OpenNotebook()
    {
        audios.clip = BookSound;
        audios.Play();
        bookWatched = true;
        BookMessage.SetActive(false);
        Notebook.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnTriggerStay(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Note":
                Message.SetActive(true);
                if (Input.GetKey("e"))
                {
                    FoundNotes[numberOfNotes] = collider.name;
                    NoteButtons[numberOfNotes].SetActive(true);
                    numberOfNotes++;
                    Destroy(collider.gameObject);
                }
                break;

            case "Ammo":
                Message.SetActive(true);
                if (Input.GetKey("e"))
                {
                    Destroy(collider.gameObject);
                    Values.AddAmmo();
                }
                break;

            case "Medpack":
                Message.SetActive(true);
                if (Input.GetKey("e"))
                {
                    Destroy(collider.gameObject);
                    Values.ChangePlayerHealth(100);
                }
                break;

            case "Trap":
                Message.SetActive(true);
                if (Input.GetKey("e"))
                {
                    Destroy(collider.gameObject);
                    Values.AddTrap();
                }
                break;

            case "Can":
                Message.SetActive(true);
                if (Input.GetKey("e"))
                {
                    Destroy(collider.gameObject);
                    Values.AddCan();
                }
                break;

            case "Rope":
                Message.SetActive(true);
                if (Input.GetKey("e"))
                {
                    Destroy(collider.gameObject);
                    Values.AddRope();
                }
                break;

            case "Stick":
                Message.SetActive(true);
                if (Input.GetKey("e"))
                {
                    Destroy(collider.gameObject);
                    Values.AddStick();
                }
                break;
            case "SpotTrap":
                BuildingMessage.SetActive(true);
                if (Values.CanPlaceTrap())
                {
                    BuildingMessage.GetComponent<Text>().text = "Press 'E' to place";
                    if (Input.GetKey("e"))
                        Values.tripwire.Place(collider.gameObject.transform.parent.gameObject);
                }
                else
                    BuildingMessage.GetComponent<Text>().text = "Not enough items to place";
                break;
        }
    }
}