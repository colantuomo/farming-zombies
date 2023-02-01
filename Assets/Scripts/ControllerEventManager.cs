using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerEventManager : MonoBehaviour
{
    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("cancel?");
            GameplayEvents.Instance.RotateItem();
        }
    }

}
