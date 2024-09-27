using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRouteController : MonoBehaviour
{
    [SerializeField] List<Vector3> PatrolRoute;
    [SerializeField] NavMeshAgent _navMeshAgent;

    public event Action RouteComplete;
    public Action RouteChanged;

    Vector3[] _waypoints;
    int _actualWaypointIndex;

    void Start()
    {
        
    }

    void Update()
    {
        _waypoints = _navMeshAgent.path.corners;
    }

    public Vector3 GetNextPoint()
    {
        _actualWaypointIndex++;
        if(_actualWaypointIndex >= _waypoints.Length)
        {
            RouteComplete?.Invoke();
            return Vector3.zero;
        }
        return _waypoints[_actualWaypointIndex--];
    }
}
