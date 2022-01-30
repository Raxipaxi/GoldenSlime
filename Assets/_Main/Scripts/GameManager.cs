using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void MainMenue()
    {
        SceneManager.LoadScene("MainMenue");
    }
    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }
    public void LoseScreen()
    {
        SceneManager.LoadScene("Lose");
    }
}
