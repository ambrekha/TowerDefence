using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRepeat());
    }

    // coroutine going on forever to spawn enemies every X seconds
    IEnumerator SpawnRepeat()
    {

        while(true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity); // Quaternion.identity -> no rotation
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
