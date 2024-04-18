using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{
    public void OnConfirm()
    {
        SceneManager.UnloadSceneAsync("Tutorial");
    }
}
