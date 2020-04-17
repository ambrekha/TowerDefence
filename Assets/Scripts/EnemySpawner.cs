using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnedEnemies;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRepeat());
        spawnedEnemies.text = score.ToString();
    }

    // coroutine going on forever to spawn enemies every X seconds
    IEnumerator SpawnRepeat()
    {

        while(true)
        {
            score++;
            spawnedEnemies.text = score.ToString();
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity); // Quaternion.identity -> no rotation
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
