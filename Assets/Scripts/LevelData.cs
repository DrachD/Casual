using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string levelName;
    public Vector3 pointOfSpawnHero;
}

////////////////////////////////////////////////////////
// main class for level control
////////////////////////////////////////////////////////

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int maxLevels = 3;
    public int selectedLevel = 1;
    public int currentLevel = 1;
    [SerializeField] Level[] levels;

    public Level[] Levels => levels;

    public void SetLevel(int value)
    {
        selectedLevel = value;
    }
}
