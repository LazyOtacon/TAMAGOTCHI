using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DilemmaHandler : MonoBehaviour
{
    public void CloseWindow()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void AddMorale(int morale) 
    {
        PlayerPrefs.SetInt(NumberController.MORALESTAT, PlayerPrefs.GetInt(NumberController.MORALESTAT) + morale);
    }

    public void RemoveMorale(int morale) 
    {
        PlayerPrefs.SetInt(NumberController.MORALESTAT, PlayerPrefs.GetInt(NumberController.MORALESTAT) - morale);
    }

    public void AddInfluence(int influence) 
    {
        PlayerPrefs.SetInt(NumberController.INFLUENCESTAT, PlayerPrefs.GetInt(NumberController.INFLUENCESTAT) + influence);
    }

    public void RemoveInfluence(int influence) 
    {
        PlayerPrefs.SetInt(NumberController.INFLUENCESTAT, PlayerPrefs.GetInt(NumberController.INFLUENCESTAT) - influence);
    }

    public void ShowOutcome(GameObject outcome)
    { 
        outcome.SetActive(true);
    }
}
