using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPatrolManager : MonoBehaviour
{
    [SerializeField] private PatrolPoints[] patrolPointsList;
    private readonly List<Transform> _allPatrolPoints = new List<Transform>();
    private void Awake()
    {
        foreach (var patrolPoints in patrolPointsList)
        {
            foreach (var patrolTransform in patrolPoints.transforms)
            {
                _allPatrolPoints.Add(patrolTransform);
            }
        }
    }

    public PatrolPoints GetPatrolPoints()
    {
        return patrolPointsList[Random.Range(0, patrolPointsList.Length)];
    }

    public Transform GetRandomPatrolPoint()
    {
        return _allPatrolPoints[Random.Range(0, _allPatrolPoints.Count)];
    }
}
