using UnityEngine;
using System.Collections;

public class Labels3D : MonoBehaviour
{
    public float maxDistance = 6;
    private GameObject Player;
    private GameObject[] Labels;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Labels = GameObject.FindGameObjectsWithTag("Label");
    }

    void FixedUpdate()
    {
        foreach (GameObject item in Labels)
        {
            if (Vector3.Distance(item.transform.position, Player.transform.position) < maxDistance)
            {
                item.GetComponent<SpriteRenderer>().enabled = true;
                item.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
            else
            {
                item.GetComponent<SpriteRenderer>().enabled = false;
                item.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }
    }

    void Update()
    {
        checkDistance();
    }

    void checkDistance()
    {
        foreach (Structure item in Values.structure)
            changeInfo(item.GetLabel(), item.displayName + "NEWLINEHP: " + item.buildingHealth + "/500");
    }

    public void changeInfo(GameObject label, string text)
    {
        // type NEWLINE for \n
        text = text.Replace("NEWLINE", "\n");
        TextMesh textField = label.GetComponentInChildren<TextMesh>();
        textField.text = text;
    }
}
