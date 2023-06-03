using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManger : MonoBehaviour
{
    //Static refereence to the GlobalReference instance
    public static AmmoManger Instance { get; set; }

    //UI
    public TextMeshProUGUI ammoDisplay;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //If there is already an instance of GlobalReferenec and it is not this one, destory this object
            Destroy(gameObject);
        }
        else
        {
            //Set the Instance to this Object
            Instance = this;
        }
    }

}
