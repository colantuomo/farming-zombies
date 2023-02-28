using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform _cannon;
    [SerializeField]
    private Transform _pivotToRotate;
    [SerializeField]
    private float _sphereSize;
    [SerializeField]
    private float _damage = 2f;
    [SerializeField]
    private float _shootDelay = 2f;
    private float _timeRemaining;
    private Tween _shootAnimationTween;
    void Start()
    {
        _timeRemaining = 0f;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereSize, LayerMask.GetMask("Enemy"));
        if (colliders.Length > 0 && colliders[0] != null)
        {
            var enemy = colliders[0];
            //print($"Enemy founded!: {enemy.transform.name}");
            print($"Time remaining to shoot: {_timeRemaining}");
            transform.DOLookAt(enemy.transform.position, .5f);
            if (_timeRemaining >= 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = _shootDelay;
                Shoot(enemy.transform);
            }
        }
    }

    private void Shoot(Transform target)
    {
        print("Has shoot!");
        target.TryGetComponent(out EnemyBehavior enemyBehavior);
        if (enemyBehavior != null)
        {
            enemyBehavior.TakeDamage(_damage);
        }
        _shootAnimationTween.Kill();
        _shootAnimationTween = _cannon.DOLocalMoveZ(-0.07f, .1f).OnComplete(() =>
        {
            _cannon.DOLocalMoveZ(0f, 1f).SetEase(Ease.OutBounce);
        });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sphereSize);
    }
}
