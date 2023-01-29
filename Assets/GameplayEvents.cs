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

    public event Action OnCancelAction;
    public void CancelAction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            print("cancel?");
            OnCancelAction?.Invoke();
        }
    }
}
