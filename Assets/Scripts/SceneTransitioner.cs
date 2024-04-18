using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    public string sceneName;
    public Button continueGame;
    public static string EXISTING_GAME = "newGameStarted";
    private void Start()
    {
        GameObject buttonObject = continueGame.gameObject;
        if (PlayerPrefs.GetInt(EXISTING_GAME) == 1)
        {
            buttonObject.SetActive(true);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt(NumberController.INFLUENCESTAT, 0);
        PlayerPrefs.SetInt(NumberController.MORALESTAT, 100);
        PlayerPrefs.SetFloat(NumberController.HEALTHSTAT, 100);
        PlayerPrefs.SetInt(NumberController.DUDECOUNTER, 1);
        PlayerPrefs.SetInt(EXISTING_GAME, 1);

        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
