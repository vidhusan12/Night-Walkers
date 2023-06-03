using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    //Static refereence to the GlobalReference instance
    public static SoundManger Instance { get; set; }

    //M1911 Sounds
    public AudioSource shootingSoundM1911;
    public AudioSource reloadingSoundM1911;
    public AudioSource emptyMagazineSoundM1911;

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
