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
            GameplayEvents.Instance.RotateItem();
        }
    }

    public void OnCancelAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameplayEvents.Instance.CancelAction();
        }
    }

    public void OnMouseClicked(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameplayEvents.Instance.Click();
        }
    }

    public void OnDeleteAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameplayEvents.Instance.Delete();
        }
    }

}
