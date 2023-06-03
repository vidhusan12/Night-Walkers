using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Guns;

public class SoundManger : MonoBehaviour
{
    //Static refereence to the GlobalReference instance
    public static SoundManger Instance { get; set; }

    //Shooting Channel
    public AudioSource ShootingChannel;
    //Common empty magazine sound
    public AudioSource emptyMagazineSoundM1911;

    //M1911 Sounds
    public AudioClip M1911Shot;
    public AudioSource reloadingSoundM1911;
    
    //AK74 Sounds
    public AudioClip AK74Shot;
    public AudioSource reloadingSoundAK74;


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

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                ShootingChannel.PlayOneShot(M1911Shot);
                break;
            case WeaponModel.AK74:
                ShootingChannel.PlayOneShot(AK74Shot);
                break;
        }

    }

    public void PlayReloadingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                reloadingSoundM1911.Play();
                break;
            case WeaponModel.AK74:
                reloadingSoundAK74.Play();
                break;
        }

    }
}
