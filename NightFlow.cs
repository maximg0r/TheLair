using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NightFlow : MonoBehaviour
{

    private Text TimeBar;
    private Text nightCahgeText;
    [SerializeField]
    private GameObject nightChange;
    [SerializeField]
    private GameObject nightChangePanel;
    int nightNum = 1;

    private float min;
    private float minMax = 17f;
    private float sec = 0.0f;
    private float secMax = 59.0f;

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Fett;
    private Vector3 playerStart;
    private Vector3 fettStart;

    void Start()
    {
        playerStart = Player.transform.position;
        fettStart = Fett.transform.position;

        TimeBar = GameObject.FindWithTag("TimeBar").GetComponent<Text>();
        nightCahgeText = nightChange.GetComponent<Text>();
        sec = secMax;
        min = minMax;
    }

    void Update()
    {
        if (nightChangePanel.activeSelf)
            StartCoroutine(ShowNight());

        sec -= Time.deltaTime;
        if (sec <= 0)
        {
            min--;
            sec = secMax;
        }
        if (min < 0)
        {
            nightNum++;
            min = minMax;
            nightChangePanel.SetActive(true);
            nightCahgeText.text = "Night " + nightNum;
            Player.transform.position = playerStart;
            Fett.transform.position = fettStart;
            FettNav.chasing = false;
        }

        if (sec < 9.5)
            TimeBar.text = "Night " + nightNum + " " + min + ":0" + Mathf.Round(sec);
        else
            TimeBar.text = "Night " + nightNum + " " + min + ":" + Mathf.Round(sec);
    }

    private IEnumerator ShowNight()
    {
        yield return new WaitForSeconds(4.5f);
        nightChangePanel.SetActive(false);
        sec = secMax;
    }
}
