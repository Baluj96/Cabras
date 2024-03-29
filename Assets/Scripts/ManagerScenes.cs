using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScenes : MonoBehaviour
{
    public static ManagerScenes instance;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.anyKeyDown)
            {
                LoadLevel(0);
            }
        }
    }

    public void LoadLevel(int indexLevel)
    {
        SceneManager.LoadSceneAsync(indexLevel);
    }

    public void DoReLoad()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
