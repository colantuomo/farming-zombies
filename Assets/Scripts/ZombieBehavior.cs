using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehavior : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _anim;
    [SerializeField]
    private float _searchRadius = 1f;
    [SerializeField]
    private float _attackDelay = 1.5f;
    private IEnumerator _attackCoroutine;
    private EnemyBehavior _enemyBehavior;

    void Start()
    {
        _enemyBehavior = GetComponent<EnemyBehavior>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_enemyBehavior.IsDead())
        {
            _navMeshAgent.enabled = false;
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRadius, LayerMask.GetMask("Player", "Gun"));
        var hasATarget = colliders.Length > 0 && colliders[0] != null;
        if (hasATarget)
        {
            var target = colliders[0];
            _navMeshAgent.SetDestination(target.transform.position);
            var isCloseToTarget = _navMeshAgent.remainingDistance != 0 && _navMeshAgent.remainingDistance <= 1f;
            //print($"_navMeshAgent.remainingDistance: {_navMeshAgent.remainingDistance} - isCloseToTarget: {isCloseToTarget}");
            if (isCloseToTarget)
            {
                _navMeshAgent.isStopped = true;
                _anim.SetBool("isWalking", false);
                if (_attackCoroutine == null)
                {
                    _attackCoroutine = Attack();
                    StartCoroutine(_attackCoroutine);
                }
                return;
            }
            if (_attackCoroutine != null)
            {
                _anim.ResetTrigger("Attack");
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
            _navMeshAgent.isStopped = false;
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _navMeshAgent.isStopped = true;
            _anim.SetBool("isWalking", false);
        }
    }

    IEnumerator Attack()
    {
        for (; ; )
        {
            print("Attack?");
            _anim.SetTrigger("Attack");
            yield return new WaitForSeconds(_attackDelay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }
}
