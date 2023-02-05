using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using static UnityEditor.Progress;

public class WorldBuildingHandler : MonoBehaviour
{
    [SerializeField]
    private Transform buildings_Fence;

    private Transform _lastClickedAvailableGround;
    private Transform _currentSelectedItem;

    private void Start()
    {
        GameplayEvents.Instance.OnChooseAnItemShop += OnChooseAnItemShop;
        GameplayEvents.Instance.OnEditingItem += OnEditingItem; ;
        GameplayEvents.Instance.OnRotateItem += OnRotateItem;
        GameplayEvents.Instance.OnCancelAction += OnCancelAction;
        GameplayEvents.Instance.OnDelete += OnDelete;
    }

    private void OnDelete()
    {
        if (IsEditingAndHaveAnItemSelected())
        {
            Destroy(_currentSelectedItem.gameObject);
            GameplayEvents.Instance.CancelAction();
            MakeLastClickedGroundAvailable();
        }
    }

    private void OnEditingItem(Transform item)
    {
        GameState.Instance.SetEditingState();
        _currentSelectedItem = item;
    }

    private void OnCancelAction()
    {
        _currentSelectedItem = null;
    }

    private void OnRotateItem()
    {
        if (GameState.Instance.IsShopping()) return;
        if (_currentSelectedItem == null) return;
        _currentSelectedItem.RotateAround(_currentSelectedItem.position, Vector3.up, 45);
    }

    private void OnChooseAnItemShop(string itemName)
    {
        GameState.Instance.SetEditingState();
        _lastClickedAvailableGround.transform.gameObject.layer = LayerMask.NameToLayer(MyLayers.GroundWithEditableItem);
        var posToPlace = new Vector3(_lastClickedAvailableGround.position.x, _lastClickedAvailableGround.position.y, (_lastClickedAvailableGround.position.z - 0.25f));
        _currentSelectedItem = Instantiate(buildings_Fence, posToPlace, Quaternion.identity);
        _currentSelectedItem.gameObject.isStatic = true;
    }

    private void MakeLastClickedGroundAvailable()
    {
        _lastClickedAvailableGround.transform.gameObject.layer = LayerMask.NameToLayer(MyLayers.GroundAvailable);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var item = GetMouseClickedObject();
            print($"Item {item.transform.name}");
            if (GlobalLayers.IsEditableItem(item.transform.gameObject.layer))
            {
                GameplayEvents.Instance.EditingItem(item.transform);
                return;
            }
            if (GlobalLayers.IsGroundAvailable(item.transform.gameObject.layer))
            {
                _lastClickedAvailableGround = item.transform;
                GameplayEvents.Instance.AddingGroundItem(item.transform);
                return;
            }
        }
    }

    private bool IsEditingAndHaveAnItemSelected()
    {
        return GameState.Instance.IsEditing() && _currentSelectedItem != null;
    }

    private RaycastHit GetMouseClickedObject()
    {
        Vector2 pos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0.0f));
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }

}
