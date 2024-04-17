using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI influence;
    public TextMeshProUGUI morale;
    public TextMeshProUGUI health;
    public static string HEALTHSTAT = "health";
    public static string INFLUENCESTAT = "influence";
    public static string MORALESTAT = "morale";
    public static string MONEYSTAT = "money";
    public static string DUDECOUNTER = "soldiers";

    private void Start()
    {
        money.text = "$" +PlayerPrefs.GetInt(MONEYSTAT);
        influence.text = ""+PlayerPrefs.GetInt(INFLUENCESTAT);
        morale.text = "" + PlayerPrefs.GetInt(MORALESTAT);
        health.text = PlayerPrefs.GetFloat(HEALTHSTAT)+"%";
    }

}
