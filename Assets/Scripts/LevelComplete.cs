using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// the game flag is the point for passing the level, as soon as we touch it, 
// our level of play increases
////////////////////////////////////////////////////////
public class LevelComplete : MonoBehaviour
{
    [SerializeField] LevelData _levelData;

    [Header("What`s the next level?")]
    [SerializeField] int nextLevel = 1;

    public void NextLevel()
    {
        if (_levelData.currentLevel < nextLevel && nextLevel <= _levelData.maxLevels)
        {
            _levelData.currentLevel = nextLevel;
        }
    }
}
