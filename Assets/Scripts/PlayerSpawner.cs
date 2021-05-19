using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// class for managing the state of the player
////////////////////////////////////////////////////////
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] float _timeToAppear = 1f;
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] GameObject _particle;
    [SerializeField] LevelData _levelData;
    private GameObject _particleObj;
    private void Start()
    {
        // Сreating a particle at the place where the character appears
        _particleObj = Instantiate(_particle, _levelData.Levels[_levelData.selectedLevel - 1].pointOfSpawnHero, Quaternion.identity);
        StartCoroutine(Spawn());
    }

    // we destroy the particle and create the character
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_timeToAppear);
        Destroy(_particleObj);
        Instantiate(_playerPrefab, _levelData.Levels[_levelData.selectedLevel - 1].pointOfSpawnHero, Quaternion.identity);
    }
}
