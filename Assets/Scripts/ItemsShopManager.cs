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

    public void BuySeedShop(string itemName)
    {
        //TODO: refact these events name and maybe use a single evente for item shop
        print($"BuySeedShop? {itemName}");
        GameplayEvents.Instance.ChooseAnSeedShop(itemName);
        _visualManager.HideItemShop();
    }

    public void BuyGunShop(string itemName)
    {
        //TODO: refact these events name and maybe use a single evente for item shop
        print($"BuyGunShop? {itemName}");
        GameplayEvents.Instance.ChooseAnGunShop(itemName);
        _visualManager.HideItemShop();
    }

}
