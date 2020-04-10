using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;

    // State of each tower
    Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();

        if(targetEnemy)
        {
            objectToMove.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>(); // find all enemies

        if(sceneEnemies.Length == 0) // if no enemies
        {
            return;
        }

        Transform closest = sceneEnemies[0].transform;

        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closest = GetClosest(closest, testEnemy.transform);
        }

        targetEnemy = closest;
    }

    private Transform GetClosest(Transform a1, Transform a2)
    {
        var dist1 = Vector3.Distance(transform.position, a1.position);
        var dist2 = Vector3.Distance(transform.position, a2.position);

        if (dist1 < dist2) return a1;
        return a2;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if(distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool v)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = v;
    }
}
