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
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.point;
    }

    public void OnMouseClicked(InputAction.CallbackContext context)
    {
        if (!GameState.Instance.IsPlaying()) return;
        if (context.performed)
        {
            _anim.Play("Run");
            print($"Works??");
            var clickPos = GetObjectPosition();
            _navMeshAgent.SetDestination(clickPos);
        }
    }
}
