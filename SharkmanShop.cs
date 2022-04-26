using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkmanShop : MonoBehaviour
{
    //check for collision
    //check if player collided
    //if player, check for E key press
    //check if player has coin
    //remove coin, update inventory
    //play win sound
    //else no coin? Debug "no coin" dialogue

    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(uiManager == null)
        {
            Debug.LogError("UI Manager is NULL.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if(player != null)
                {
                    if (player.hasCoin == true)
                    {
                        player.hasCoin = false;
                        uiManager.SpentCoin();
                        player.EnableWeapon();
                        Debug.Log("Sharkman: Pleasure doing business with you!");
                        AudioSource winSound = GetComponent<AudioSource>();
                        winSound.Play();
                    }
                    else
                    {
                        Debug.Log("Sharkman: Don't waste my time, broke boi.");
                    }
                }
            }
        }
    }
}
