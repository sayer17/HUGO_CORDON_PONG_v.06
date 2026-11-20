using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class game_manager : MonoBehaviour
{
    public int PuntosP1=0;
    public int PuntosP2=0;
    public string PointsCount;
    public TextMeshProUGUI marcadorText;
    public GameObject WinPanel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    public void UpdatePointsText()
    {
        WinMenu();
        PointsCount = "0" + PuntosP1 + "|" + "0" + PuntosP2;
        marcadorText.text = PointsCount;
    }
    public void WinMenu()
    {
        if (PuntosP1 ==4 || PuntosP2 ==4)
        {
            WinPanel.SetActive(true); 
            Time.timeScale = 0f;
        }
    }

}
