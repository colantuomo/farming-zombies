using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class VisualManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _itemShopPanel;
    private RectTransform _itemShopRectTransform;

    public Ease itemShopAnimEase = Ease.OutFlash;
    public float itemShopAnimSpeed = .5f;

    private Tween _itemShopTween = null;
    void Start()
    {
        _itemShopRectTransform = _itemShopPanel.GetComponent<RectTransform>();
        GameplayEvents.Instance.OnAddingGroundItem += OnAddingGroundItem;
        GameplayEvents.Instance.OnCancelAction += OnCancelAction;
    }

    private void OnAddingGroundItem(Transform clickedItem)
    {
        _itemShopTween.Kill();
        _itemShopPanel.SetActive(true);
        _itemShopTween = _itemShopRectTransform.DOAnchorPosY(0, itemShopAnimSpeed).SetEase(itemShopAnimEase);
    }

    private void OnCancelAction()
    {
        _itemShopTween.Kill();
        float offScreenHeigth = -(Screen.height * 2);
        _itemShopTween = _itemShopRectTransform.DOAnchorPosY(offScreenHeigth, itemShopAnimSpeed).SetEase(itemShopAnimEase).OnComplete(() =>
        {
            _itemShopPanel.SetActive(false);
        });
    }

    public void HideItemShop()
    {
        float offScreenHeigth = -(Screen.height * 2);
        _itemShopTween = _itemShopRectTransform.DOAnchorPosY(offScreenHeigth, itemShopAnimSpeed).SetEase(itemShopAnimEase);
    }
}
