using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Cinemachine;

public class NavmeshController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _anim;
    [SerializeField]
    private Camera _virtualCam;
    void Start()
    {
        GameplayEvents.Instance.OnClick += OnClick;
        GameplayEvents.Instance.OnCancelAction += OnCancelAction;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    private void OnCancelAction()
    {
        _navMeshAgent.SetDestination(transform.position);
    }

    void Update()
    {
        if (!_navMeshAgent.hasPath)
        {
            _anim.Play("Idle");
        }

    }

    private Vector3 GetObjectPosition()
    {
        Vector2 pos = Input.mousePosition;
        Ray ray = _virtualCam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0.0f));
        Physics.Raycast(ray, out RaycastHit hit);
        return hit.point;
    }

    public void OnClick()
    {
        print($"Game State {GameState.Instance.Current()}");
        if (GameState.Instance.IsPlaying())
        {
            _anim.Play("Run");
            print($"Works??");
            var clickPos = GetObjectPosition();
            _navMeshAgent.SetDestination(clickPos);
        }
        if (GameState.Instance.IsEditing())
        {
            GameplayEvents.Instance.CancelAction();
            return;
        }

    }
}
