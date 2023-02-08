using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameplayEvents : MonoBehaviour
{
    public static GameplayEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public event Action<Transform> OnAddingGroundItem;
    public void AddingGroundItem(Transform item)
    {
        OnAddingGroundItem?.Invoke(item);
    }

    public event Action<Transform> OnEditingItem;
    public void EditingItem(Transform item)
    {
        OnEditingItem?.Invoke(item);
    }

    public event Action OnCancelAction;
    public void CancelAction()
    {
        print("cancel?");
        OnCancelAction?.Invoke();
    }

    public event Action<string> OnChooseAnItemShop;
    public void ChooseAnItemShop(string item)
    {
        print($"selected an item? {item}");
        OnChooseAnItemShop?.Invoke(item);
    }

    public event Action<string> OnChooseAnSeedShop;
    public void ChooseAnSeedShop(string item)
    {
        print($"selected an seed? {item}");
        OnChooseAnSeedShop?.Invoke(item);
    }

    public event Action OnRotateItem;
    public void RotateItem()
    {
        OnRotateItem?.Invoke();
    }

    public event Action OnClick;
    public void Click()
    {
        OnClick?.Invoke();
    }

    public event Action OnDelete;
    public void Delete()
    {
        OnDelete?.Invoke();
    }
}
