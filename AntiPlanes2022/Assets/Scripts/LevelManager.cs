using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelOver;
    public static int planesShotDown = 0;
    public static bool gameOver = false;
    public GameObject canvasWin;
    public GameObject canvasOver;

    private void Start()
    {
        canvasOver.SetActive(false);
        canvasWin.SetActive(false);
    }

    public void Levels()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsWin()
    {
        //panelWin.SetActive(!panelWin.activeSelf);
        canvasWin.SetActive(!canvasWin.activeSelf);
    }

    public void SettingsOver()
    {
        //panelOver.SetActive(true);//!panelOver.activeSelf);
        canvasOver.SetActive(true);
    }

    private void Update()
    {
        if (planesShotDown >= 10)
        {
            SettingsWin();
            planesShotDown = 0;
            gameOver = false;
        }

        if (gameOver)
        {
            SettingsOver();
            planesShotDown = 0;
            gameOver = false;
        }
    }
}