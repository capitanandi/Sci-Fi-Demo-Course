using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinPickup;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if(player != null)
                {
                    player.CollectCoin();

                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    if(uiManager != null)
                    {
                        uiManager.CollectedCoin();
                    }

                    AudioSource.PlayClipAtPoint(_coinPickup, transform.position, 0.5f);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
