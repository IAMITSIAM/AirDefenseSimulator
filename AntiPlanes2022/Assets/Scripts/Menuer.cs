using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuer : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Выход");
        Application.Quit();
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
}
