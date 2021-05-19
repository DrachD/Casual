using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointOfView { right, left, up, down }

////////////////////////////////////////////////////////
// PathEnemy class which stores the prefab of the enemy, the path along which 
// the enemy will need to go and the starting point of the enemy's sight
////////////////////////////////////////////////////////

[CreateAssetMenu(menuName="path", fileName="path enemy")]
public class PathEnemy : ScriptableObject
{
    [SerializeField] PointOfView pointOfView;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    
    public Vector3 PointOfView
    {
        get 
        {
            switch (pointOfView)
            {
                case global::PointOfView.right:
                    return Vector3.right;
                case global::PointOfView.left:
                    return Vector3.left;
                case global::PointOfView.up:
                    return Vector3.forward;
                case global::PointOfView.down:
                    return Vector3.back;
                default:
                    return Vector3.forward;
            }
        }
    }

    public GameObject EnemyPrefab => enemyPrefab;

    public GameObject PathPrefab => pathPrefab;

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }
}
