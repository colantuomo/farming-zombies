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
        GameplayEvents.Instance.OnAddingGroundItem += OnAddingGroundItem;
        GameplayEvents.Instance.OnCancelAction += OnCancelAction;
    }

    private void OnAddingGroundItem(Transform item)
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
