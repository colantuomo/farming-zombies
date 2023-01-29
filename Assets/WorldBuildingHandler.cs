using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class WorldBuildingHandler : MonoBehaviour
{
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var item = GetMouseClickedObject();
            if(GlobalLayers.IsGroundAvailable(item.transform.gameObject.layer))
            {
                GameplayEvents.Instance.AddingGroundItem(item.transform);
            }
        }
    }

    private RaycastHit GetMouseClickedObject()
    {
        Vector2 pos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0.0f));
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }

}
