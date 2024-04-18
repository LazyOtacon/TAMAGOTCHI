using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    public int startMoney = 0;
    public float startHealth = 10;

    public TextMeshProUGUI money;
    public TextMeshProUGUI influence;
    public TextMeshProUGUI influenceGain;
    public TextMeshProUGUI morale;
    public TextMeshProUGUI health;
    //public TextMeshProUGUI satiation;
    public static string HEALTHSTAT = "health";
    public static string INFLUENCESTAT = "influence";
    public static string FOODLEFT = "satiation";
    public static string MORALESTAT = "morale";
    public static string MONEYSTAT = "money";
    public static string DUDECOUNTER = "soldiers";

    public bool combatActive;

    void SetInit() 
    {
        PlayerPrefs.SetInt(MONEYSTAT, startMoney);
        PlayerPrefs.SetInt(INFLUENCESTAT, 0);
        PlayerPrefs.SetInt(MORALESTAT, 0);
        PlayerPrefs.GetFloat(HEALTHSTAT, startHealth);
        PlayerPrefs.SetInt(DUDECOUNTER, 1);
    }

    private void Start()
    {
        SetInit();

        money.text = "" +PlayerPrefs.GetInt(MONEYSTAT);
        influence.text = ""+PlayerPrefs.GetInt(INFLUENCESTAT);
        morale.text = "" + PlayerPrefs.GetInt(MORALESTAT);
        health.text = PlayerPrefs.GetFloat(HEALTHSTAT) + "%";
        //satiation.text = PlayerPrefs.GetFloat(FOODLEFT) + "%";
        StartCoroutine(GameTick());
    }
    IEnumerator GameTick()
    {
        yield return new WaitForSeconds(1);
        ContinueStatLoop();
    }
    void ContinueStatLoop()
    {
        PlayerPrefs.SetInt(INFLUENCESTAT, PlayerPrefs.GetInt(INFLUENCESTAT) + PlayerPrefs.GetInt(DUDECOUNTER));
        influenceGain.text = "+ " + PlayerPrefs.GetInt(DUDECOUNTER);
        PlayerPrefs.SetFloat(FOODLEFT, PlayerPrefs.GetFloat(FOODLEFT) - 1);

        if (combatActive && PlayerPrefs.GetInt(DUDECOUNTER) <= 0)
        {
            PlayerPrefs.SetFloat(HEALTHSTAT, PlayerPrefs.GetFloat(HEALTHSTAT) - 1);
            if (PlayerPrefs.GetFloat(HEALTHSTAT) <= 0) GameOver(); // end game
        }

        money.text = "$" + PlayerPrefs.GetInt(MONEYSTAT);
        influence.text = "" + PlayerPrefs.GetInt(INFLUENCESTAT);
        morale.text = "" + PlayerPrefs.GetInt(MORALESTAT);
        health.text = PlayerPrefs.GetFloat(HEALTHSTAT) + "%";
        //satiation.text = PlayerPrefs.GetFloat(FOODLEFT) + "%";

        StartCoroutine(GameTick());
    }

    void GameOver() 
    { 
    
    }
}
