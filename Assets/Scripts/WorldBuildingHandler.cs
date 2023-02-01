using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

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
        GameplayEvents.Instance.OnCancelAction += OnCancelAction; ;
    }

    private void OnEditingItem(Transform item)
    {
        _currentSelectedItem = item;
    }

    private void OnCancelAction()
    {
        _lastClickedAvailableGround = null;
        _currentSelectedItem = null;
    }

    private void OnRotateItem()
    {
        if (_currentSelectedItem == null) return;
        //_currentSelectedItem.Rotate(0, 45, 0);
        _currentSelectedItem.RotateAround(_currentSelectedItem.position, Vector3.up, 45);
    }

    private void OnChooseAnItemShop(string itemName)
    {
        _lastClickedAvailableGround.transform.gameObject.layer = LayerMask.NameToLayer(MyLayers.GroundWithEditableItem);
        var posToPlace = new Vector3(_lastClickedAvailableGround.position.x, _lastClickedAvailableGround.position.y, (_lastClickedAvailableGround.position.z - 0.25f));
        _currentSelectedItem = Instantiate(buildings_Fence, posToPlace, Quaternion.identity);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var item = GetMouseClickedObject();
            if (GlobalLayers.IsGroundWithEditableItem(item.transform.gameObject.layer))
            {
                //TODO: passar o transform do objeto que está acima do "ground"
                //_lastClickedAvailableGround = item.transform;
                GameplayEvents.Instance.EditingItem(item.transform);
            }
            if (GlobalLayers.IsGroundAvailable(item.transform.gameObject.layer))
            {
                _lastClickedAvailableGround = item.transform;
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
