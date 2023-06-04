using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsSystem : MonoBehaviour
{
    public GameObject coinText;
    public int theCoins;

    void OnTriggerEnter(Collider other)
    {
        theCoins += 20;
        coinText.GetComponent<Text>().text = "COINS: " + theCoins;
        Destroy(gameObject);
    }
}
