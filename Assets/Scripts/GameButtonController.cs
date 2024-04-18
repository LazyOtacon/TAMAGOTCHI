using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//as of now, this is for testing purposes. some of it will likely be recycled into actual use, but this is testing the various stats and what happens when they change.

public class GameButtonController : MonoBehaviour
{
    public int hireCost;
    public int hireCostModifier;
    public TMP_Text hireCostText;
    public int healCost;
    public TMP_Text healCostText;
    public int healAmount;
    public int encourageCost;
    public TMP_Text moraleCostText;
    public int encourageAmount;

    [SerializeField] int maxUnits;
    [SerializeField] Transform spawner;
    [SerializeField] GameObject unitToSpawn;

    private void Start()
    {
        hireCostText.text = "" + hireCost;
    }

    public void Hire()
    {
        if (hireCost<=PlayerPrefs.GetInt(NumberController.INFLUENCESTAT) && PlayerPrefs.GetInt(NumberController.DUDECOUNTER) + 1 <= maxUnits)
        {
            PlayerPrefs.SetInt(NumberController.DUDECOUNTER, PlayerPrefs.GetInt(NumberController.DUDECOUNTER) + 1);
            PlayerPrefs.SetInt(NumberController.INFLUENCESTAT, PlayerPrefs.GetInt(NumberController.INFLUENCESTAT) - hireCost);
            hireCost += hireCostModifier;
            hireCostText.text = "" + hireCost;
            Instantiate(unitToSpawn, spawner);
        }
        
    }
    public void Death()
    {
        PlayerPrefs.SetInt(NumberController.DUDECOUNTER, PlayerPrefs.GetInt(NumberController.DUDECOUNTER) - 1);
        PlayerPrefs.SetInt(NumberController.MORALESTAT, PlayerPrefs.GetInt(NumberController.MORALESTAT) - 1);
    }
    public void Encourage()
    {
        if (encourageCost<=PlayerPrefs.GetInt(NumberController.INFLUENCESTAT))
        {
            PlayerPrefs.SetInt(NumberController.INFLUENCESTAT, PlayerPrefs.GetInt(NumberController.INFLUENCESTAT) - encourageCost);
            PlayerPrefs.SetInt(NumberController.MORALESTAT, PlayerPrefs.GetInt(NumberController.MORALESTAT) + encourageAmount);
        }
    }
    public void Heal()
    {
        if (healCost<=PlayerPrefs.GetInt(NumberController.INFLUENCESTAT))
        {
            PlayerPrefs.SetFloat(NumberController.HEALTHSTAT, PlayerPrefs.GetFloat(NumberController.HEALTHSTAT) + healAmount);
            PlayerPrefs.SetInt(NumberController.INFLUENCESTAT, PlayerPrefs.GetInt(NumberController.INFLUENCESTAT) - healCost);
        }
    }
}
