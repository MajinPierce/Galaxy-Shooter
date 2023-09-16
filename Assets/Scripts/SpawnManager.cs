using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float spawnRate = 3f;
    private bool _stopSpawning = false;
    private const float BoundY = 7.5f;
    private const float BoundX = 9f;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            GameObject newEnemy = Instantiate(
                enemyPrefab, 
                new Vector3(Random.Range(-BoundX, BoundX), BoundY, 0), 
                Quaternion.identity
                );
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(spawnRate);
            if (spawnRate >= 0.3f)
            {
                spawnRate -= 0.05f;
            }
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawning)
        {
            Instantiate(tripleShotPrefab,
                new Vector3(Random.Range(-BoundX, BoundX), BoundY, 0),
                Quaternion.identity
            );
            yield return new WaitForSeconds(Random.Range(7, 14));
        }
    }
}
