using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberController : MonoBehaviour
{
    [Header("Max Values")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] int maxInfluence = 100;
    [SerializeField] int maxMorale = 100;


    public TextMeshProUGUI influenceText;
    public TextMeshProUGUI moraleText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI influenceGain;



    [Header("Sliders")]
    public Slider influence;
    public Slider morale;
    public Slider health;

    public static string HEALTHSTAT = "health";
    public static string INFLUENCESTAT = "influence";
    public static string MORALESTAT = "morale";
    public static string DUDECOUNTER = "soldiers";

    public bool combatActive;

    [Header("Moral Dilemmas")]
    [SerializeField] string[] scenes;
    [SerializeField] int dilemmaFrequency;
    int currentCount = 0;


    void SetInit() 
    {
        PlayerPrefs.SetInt(INFLUENCESTAT, 0);
        PlayerPrefs.SetInt(MORALESTAT, 0);
        PlayerPrefs.SetFloat(HEALTHSTAT, maxHealth);
        PlayerPrefs.SetInt(DUDECOUNTER, 1);
    }

    private void Start()
    {
        SetInit();

        influence.value = PlayerPrefs.GetInt(INFLUENCESTAT);
        influenceText.text = influence.value + "/" + maxInfluence;
        morale.value = PlayerPrefs.GetInt(MORALESTAT);
        moraleText.text = morale.value + "/" + maxMorale;
        health.value = PlayerPrefs.GetFloat(HEALTHSTAT);
        healthText.text = health.value + "/" + maxHealth;
        StartCoroutine(GameTick());
    }
    IEnumerator GameTick()
    {
        yield return new WaitForSeconds(1);

        if (currentCount == dilemmaFrequency)
        {
            SpawnDilemma();
            currentCount = 0;
            dilemmaFrequency = Random.Range(dilemmaFrequency, dilemmaFrequency*2 +1);
        }
        currentCount++;
        ContinueStatLoop();
    }

    void SpawnDilemma() 
    {
        SceneManager.LoadSceneAsync(scenes[Random.Range(0, scenes.Length)], LoadSceneMode.Additive);
    }
    void ContinueStatLoop()
    {
        PlayerPrefs.SetInt(INFLUENCESTAT, PlayerPrefs.GetInt(INFLUENCESTAT) + PlayerPrefs.GetInt(DUDECOUNTER));
        influenceGain.text = "+ " + PlayerPrefs.GetInt(DUDECOUNTER);

        if (combatActive && PlayerPrefs.GetInt(DUDECOUNTER) <= 0)
        {
            PlayerPrefs.SetFloat(HEALTHSTAT, PlayerPrefs.GetFloat(HEALTHSTAT) - 1);
            if (PlayerPrefs.GetFloat(HEALTHSTAT) <= 0) GameOver(); // end game
        }

        // Update Sliders
        influence.value = PlayerPrefs.GetInt(INFLUENCESTAT);
        morale.value = PlayerPrefs.GetInt(MORALESTAT);
        health.value = Mathf.Round(PlayerPrefs.GetFloat(HEALTHSTAT));

        // Update Text
        influenceText.text = influence.value + "/" + maxInfluence;
        moraleText.text = morale.value + "/" + maxMorale;
        healthText.text = health.value + "/" + maxHealth;
        StartCoroutine(GameTick());
    }

    void GameOver() 
    { 
    
    }
}
