using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    float _totalLife = 5f;
    [SerializeField]
    float _timeToDie = 10f;
    private Animator _anim;
    private CapsuleCollider _collider;
    private SkinnedMeshRenderer _skinnedMesh;

    private void Start()
    {
        _skinnedMesh = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        _collider = GetComponent<CapsuleCollider>();
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        _totalLife -= damage;
        if (IsDead())
        {
            GameplayEvents.Instance.EnemyGotKilled(transform);
            Die();
        }
        else
        {
            TakeHit();
        }
    }

    private void TakeHit()
    {
        // trigger take hit animation
        _anim.SetLayerWeight(1, 1);
        _anim.SetTrigger("Hit");
        _skinnedMesh.material.color = Color.red;
        _skinnedMesh.material.DOColor(Color.white, .9f).SetEase(Ease.OutFlash).OnComplete(() =>
        {
            DOVirtual.Float(1f, 0, 1f, value =>
            {
                _anim.SetLayerWeight(1, value);
            });
        });
    }

    private void Die()
    {
        // trigger some particle
        _collider.enabled = false;
        _anim.SetTrigger("Die");
        Destroy(gameObject, _timeToDie);
    }

    public bool IsDead()
    {
        return _totalLife <= 0;
    }
}
