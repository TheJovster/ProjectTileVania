using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPeriscope : MonoBehaviour
{
    EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemyMovement.PeriscopeFunction(other);
    }
}
