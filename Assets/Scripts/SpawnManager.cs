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
    private GameObject speedBoostPrefab;
    [SerializeField]
    private float spawnRate = 3f;
    private bool _stopSpawning = false;
    private const float BoundY = 7.5f;
    private const float BoundX = 9f;

    private List<GameObject> powerups = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        powerups.Add(tripleShotPrefab);
        powerups.Add(speedBoostPrefab);
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
            var randomPowerup = Random.Range(0, powerups.Count);
            Instantiate(powerups[randomPowerup],
                new Vector3(Random.Range(-BoundX, BoundX), BoundY, 0),
                Quaternion.identity
            );
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }
}
