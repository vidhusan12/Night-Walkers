using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsSystem : MonoBehaviour
{
    public Text coinText; // Changed the type to Text
    public int theCoins;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theCoins += 20;
            coinText.text = "COINS: " + theCoins;
            Destroy(gameObject);
        }
    }
}
