using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is used for Loading the Scenes
/// </summary>

public class LevelController : MonoBehaviour
{

    [SerializeField] float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Gameplay Scene");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds); 
        SceneManager.LoadScene("Game Over");
    }

}
