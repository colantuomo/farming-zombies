using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsShopManager : MonoBehaviour
{
    [SerializeField]
    private VisualManager _visualManager;
    private void OnEnable()
    {
        print("Item Shop Enabled");
    }

    private void OnDisable()
    {
        print("Item Shop Disabled");
    }

    public void BuyItemShop(string itemName)
    {
        print($"BuyItemShop? {itemName}");
        GameplayEvents.Instance.ChooseAnItemShop(itemName);
        _visualManager.HideItemShop();
    }

}
