using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundPlaceholder : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Tween _FXTween = null;
    [SerializeField]
    private float _fxSpeed = .2f;
    [SerializeField]
    private float _searchItemRadius = .2f;
    [SerializeField]
    private Transform _searchForItemPoint;

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
            _FXTween.Kill();
            _FXTween = _meshRenderer.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), _fxSpeed);
        }
    }

    private void OnMouseExit()
    {
        if (GameState.Instance.IsPlaying() || GameState.Instance.IsEditing() || GameState.Instance.IsShopping())
        {
            _FXTween.Kill();
            _FXTween = _meshRenderer.transform.DOScale(new Vector3(0, 0, 0), _fxSpeed).OnComplete(() =>
                {
                    _meshRenderer.enabled = false;
                    float size = 0.1f;
                    _meshRenderer.transform.localScale = new Vector3(size, size, size);
                });
        }
    }

    public bool HasAnItem()
    {
        Collider[] colliders = Physics.OverlapSphere(_searchForItemPoint.position, _searchItemRadius);
        return colliders.Length > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_searchForItemPoint.position, _searchItemRadius);
    }
}
