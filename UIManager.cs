using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _inventoryCoin;

    private void Start()
    {
        _ammoText.text = "Ammo: " + 50.ToString();
    }

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count.ToString();
    }

    public void CollectedCoin()
    {
        _inventoryCoin.SetActive(true);
    }

    public void SpentCoin()
    {
        _inventoryCoin.SetActive(false);
    }
}
