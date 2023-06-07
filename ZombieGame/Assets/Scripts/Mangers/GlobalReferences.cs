using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * This script ensures that there is only one instance of the GlobalReferences object in the scene.
 * It provides a static reference to itself through the Instance property, allowing other scripts
 * to access its variables and functions.
 */

public class GlobalReferences : MonoBehaviour
{
    //Static refereence to the GlobalReference instance
    public static GlobalReferences Instance { get; set; }

    public GameObject impactEffectPrefab;


    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        if(Instance != null && Instance != this)
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
