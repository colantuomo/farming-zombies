using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundPlaceholder : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Tween FXTween = null;
    [SerializeField]
    private float fxSpeed = .2f;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        GameplayEvents.Instance.OnCancelAction += OnCancelAction;
    }

    private void OnDisable()
    {
        GameplayEvents.Instance.OnCancelAction -= OnCancelAction;
    }

    private void OnCancelAction()
    {
        _meshRenderer.enabled = false;
    }

    private void OnMouseEnter()
    {
        if (GameState.Instance.IsPlaying() || GameState.Instance.IsEditing())
        {
            _meshRenderer.enabled = true;
            FXTween.Kill();
            FXTween = _meshRenderer.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), fxSpeed);
        }
    }

    private void OnMouseExit()
    {
        if (GameState.Instance.IsPlaying() || GameState.Instance.IsEditing())
        {
            FXTween.Kill();
            FXTween = _meshRenderer.transform.DOScale(new Vector3(0, 0, 0), fxSpeed).OnComplete(() =>
                {
                    _meshRenderer.enabled = false;
                    float size = 0.1f;
                    _meshRenderer.transform.localScale = new Vector3(size, size, size);
                });
        }
    }
}
