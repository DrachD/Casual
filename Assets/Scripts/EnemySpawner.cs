using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// making all enemies
////////////////////////////////////////////////////////
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<PathEnemy> pathEnemies;
    [SerializeField] int startingWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }
    
    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < pathEnemies.Count; waveIndex++)
        {
            var currentWave = pathEnemies[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(PathEnemy wave)
    {
        var enemy = Instantiate(wave.EnemyPrefab, wave.GetWaypoints()[0].transform.position, Quaternion.LookRotation(wave.PointOfView));
        enemy.GetComponent<EnemyPathing>().SetPathEnemy(wave);
        yield return null;
    }
}
