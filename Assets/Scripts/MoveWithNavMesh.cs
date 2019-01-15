using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveWithNavMesh : MonoBehaviour {

    public Vector3 destination;
    private bool _canMove;

    public bool CanMove
    {
        set
        {
            if (value != _canMove)
            {
                _canMove = value;
                GetComponent<NavMeshAgent>().isStopped = !_canMove;
            }
        }
    }

    private void Start()
    {
        CanMove = true;
    }


    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            SetDestination();
        }
        ManageMove();
    }

    void SetDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
    }

    void ManageMove()
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
        Debug.DrawLine(transform.position, destination);
    }
}
