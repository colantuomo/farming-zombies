using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _playerFollowCam;
    [SerializeField]
    private CinemachineVirtualCamera _itemPlacementCam;

    private void Start()
    {
        GameplayEvents.Instance.OnAddingGroundItem += FocusOnSelectedItem;
        GameplayEvents.Instance.OnCancelAction += OnCancelAction;
        GameplayEvents.Instance.OnEditingItem += FocusOnSelectedItem;
    }

    private void FocusOnSelectedItem(Transform item)
    {
        _itemPlacementCam.gameObject.SetActive(true);
        _itemPlacementCam.Follow = item;
    }

    private void OnCancelAction()
    {
        _itemPlacementCam.gameObject.SetActive(false);
        _playerFollowCam.gameObject.SetActive(true);
    }
}
