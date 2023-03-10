using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Utils;

public class WorldBuildingHandler : MonoBehaviour
{
    [SerializeField]
    private Transform buildings_Fence;

    [SerializeField]
    private NavMeshSurface GameGround;

    private Transform _lastClickedAvailableGround;
    private Transform _currentSelectedItem;

    private void Awake()
    {
        //GameGround.BuildNavMesh();
    }

    private void Start()
    {
        GameGround.BuildNavMesh();
        GameplayEvents.Instance.OnChooseAnItemShop += OnChooseAnItemShop;
        GameplayEvents.Instance.OnEditingItem += OnEditingItem; ;
        GameplayEvents.Instance.OnRotateItem += OnRotateItem;
        GameplayEvents.Instance.OnCancelAction += OnCancelAction;
        GameplayEvents.Instance.OnDelete += OnDelete;
        GameplayEvents.Instance.OnChooseAnSeedShop += OnChooseAnSeedShop;
        GameplayEvents.Instance.OnChooseAnGunShop += OnChooseAnGunShop;
        GameplayEvents.Instance.OnMonsterHasBeenSummon += OnMonsterHasBeenSummon;
    }

    private void OnMonsterHasBeenSummon()
    {
        print("Monster was summon");
        GameGround.BuildNavMesh();
    }

    private void OnChooseAnGunShop(string itemName)
    {
        print($"Gun Selected: {itemName}");
        Transform itemPrefab = GetGunPrefabByName(itemName);
        InstantiateShopItem(itemPrefab);
    }

    private void OnDelete()
    {
        if (IsEditingAndHaveAnItemSelected())
        {
            Destroy(_currentSelectedItem.gameObject);
            //GameplayEvents.Instance.CancelAction();
            MakeLastClickedGroundAvailable();
            GameGround.BuildNavMesh();
        }
    }

    private void OnEditingItem(Transform item)
    {
        GameState.Instance.SetEditingState();
        _currentSelectedItem = item;
    }

    private void OnCancelAction()
    {
        if (_currentSelectedItem != null)
        {
            GameGround.BuildNavMesh();
        }
        _currentSelectedItem = null;
    }

    private void OnRotateItem()
    {
        if (GameState.Instance.IsShopping()) return;
        if (_currentSelectedItem == null) return;
        _currentSelectedItem.RotateAround(_currentSelectedItem.GetChild(0).position, Vector3.up, 90);
    }

    private void OnChooseAnItemShop(string itemName)
    {
        Transform itemPrefab = GetItemPrefabByName(itemName);
        InstantiateShopItem(itemPrefab);
    }

    private void OnChooseAnSeedShop(string itemName)
    {
        print($"Seed Selected: {itemName}");
        Transform itemPrefab = GetSeedPrefabByName(itemName);
        InstantiateShopItem(itemPrefab);
    }

    private Transform GetItemPrefabByName(string itemName)
    {
        return Resources.Load<Transform>("Prefabs/" + itemName);
    }

    private Transform GetSeedPrefabByName(string itemName)
    {
        return Resources.Load<Transform>("Prefabs/Seeds/" + itemName);
    }

    private Transform GetGunPrefabByName(string itemName)
    {
        return Resources.Load<Transform>("Prefabs/Guns/" + itemName);
    }

    private void InstantiateShopItem(Transform item)
    {
        print($"InstantiateShopItem: {item.name}");
        GameState.Instance.SetEditingState();
        _lastClickedAvailableGround.transform.gameObject.layer = LayerMask.NameToLayer(MyLayers.GroundWithEditableItem);
        ItemPositionDetails itemOffsets = item.GetComponent<ItemPositionDetails>();
        Vector3 posToPlace = new(_lastClickedAvailableGround.position.x - itemOffsets.offsets.x, _lastClickedAvailableGround.position.y - itemOffsets.offsets.y, _lastClickedAvailableGround.position.z - itemOffsets.offsets.z);
        _currentSelectedItem = Instantiate(item, posToPlace, Quaternion.identity);
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
            item.transform.TryGetComponent(out GroundPlaceholder groundPlaceholder);
            var groundHasItem = false;
            if (groundPlaceholder != null)
            {
                groundHasItem = groundPlaceholder.HasAnItem();
            }
            bool isAValidGroundAndDontHaveAnItem = !groundHasItem && groundPlaceholder != null;
            if (isAValidGroundAndDontHaveAnItem)
            {
                _lastClickedAvailableGround = item.transform;
                GameplayEvents.Instance.AddingGroundItem(item.transform);
            }
            if (GlobalLayers.IsEditableItem(item.transform.gameObject.layer))
            {
                GameplayEvents.Instance.EditingItem(item.transform);
            }
        }
    }

    private bool IsEditingAndHaveAnItemSelected()
    {
        //TODO Move this tag comparation to a Utils class
        return GameState.Instance.IsEditing() && _currentSelectedItem != null && !_currentSelectedItem.CompareTag("CantDelete");
    }

    private RaycastHit GetMouseClickedObject()
    {
        Vector2 pos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0.0f));
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }

}
