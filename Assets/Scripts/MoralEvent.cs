using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralEvent : MonoBehaviour
{
    private NumberController numberController; // Declare a variable to hold the NumberController script



    void Start()
    {
        // Find the object with the "stat" tag
        GameObject statObject = GameObject.FindGameObjectWithTag("Stats");

        // Check if the object was found
        if (statObject != null)
        {
            // Get the NumberController script from the object
            numberController = statObject.GetComponent<NumberController>();

            // Check if the script was found
            if (numberController != null)
            {
                // Now you can use numberController to access the script's variables and functions
                Debug.Log("Found NumberController script!");
            }
            else
            {
                Debug.LogError("NumberController script not found on the object with the 'stat' tag!");
            }
        }
        else
        {
            Debug.LogError("Object with the 'stat' tag not found!");
        }
    }

    public int Statchange(int TargetStat, int Change)
    {
        TargetStat += Change;

        return (TargetStat);
    }

}