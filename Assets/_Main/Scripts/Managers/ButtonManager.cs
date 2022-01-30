using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        if(menuButton != null)
        {
            menuButton.onClick.AddListener(MainMenue);
        }
        exitButton.onClick.AddListener(ExitScreen);

        playButton.onClick.AddListener(PlayGame);


    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Cursor.visible = false;
    }
    public void MainMenue()
    {
        SceneManager.LoadScene("MainMenue");
        Cursor.visible = true;
    }

    public void ExitScreen()
    {
        Application.Quit();
    }
}

