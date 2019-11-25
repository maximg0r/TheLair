using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
    GameObject Player;
    [SerializeField] private GameObject Plank;
    [SerializeField] private GameObject Frame;

    private bool isBuilding = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyUp("g"))
        {
            isBuilding = !isBuilding;
            if (isBuilding)
                Frame.SetActive(true);
            else
                Frame.SetActive(false);
        }

        if (isBuilding)
            Build();
    }

    void Build()
    {
        Frame.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 2f));
        Frame.transform.rotation = Player.transform.rotation;
        if (isBuilding && Frame.transform.position.y < 0.15f)
            Frame.transform.position = new Vector3(Frame.transform.position.x, 0.15f, Frame.transform.position.z);
        if (Input.GetMouseButtonUp(0))
            Instantiate(Plank, Frame.transform.position, Frame.transform.rotation);
    }
}
