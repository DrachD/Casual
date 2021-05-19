using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// class for managing the state of the enemy
////////////////////////////////////////////////////////
public class EnemyPathing : MonoBehaviour
{
    private PathEnemy _pathEnemy;

    // all the main points on which the enemies will need to go
    [SerializeField] List<Transform> _waypoints;

    private int _waypointIndex = 0;

    private float lastWaypointSwitchTime;

    private bool start = true;

    private void Start()
    {
        lastWaypointSwitchTime = Time.time;
        _waypoints = _pathEnemy.GetWaypoints();
        if (_waypoints.Count > 1)
            StartCoroutine(MoveEnemy());
    }

    public void SetPathEnemy(PathEnemy pathEnemy)
    {
        _pathEnemy = pathEnemy;
    }

    // movement of the enemy by points
    IEnumerator MoveEnemy()
    {
        while (true)
        {
            Vector3 targetPosition;
            Vector3 newDirection;

            if (start)
            {
                if (_waypointIndex <= _waypoints.Count - 1)
                {
                    Move(out targetPosition, out newDirection);
                    RotateIntoMoveDirection(newDirection);

                    if (transform.position == targetPosition)
                    {
                        if (_waypointIndex != 0)
                            yield return new WaitForSeconds(_waypoints[_waypointIndex].GetComponent<Point>().Delay);
                        _waypointIndex++;
                    }
                }
                else
                {
                    start = false;    
                    _waypointIndex--;
                }
            }
            else
            {
                if (_waypointIndex >= 0)
                {
                    Move(out targetPosition, out newDirection);
                    RotateIntoMoveDirection(newDirection);

                    if (transform.position == targetPosition)
                    {
                        if (_waypointIndex != _waypoints.Count - 1)
                            yield return new WaitForSeconds(_waypoints[_waypointIndex].GetComponent<Point>().Delay);
                        _waypointIndex--;
                    }
                }
                else
                {
                    start = true;
                    _waypointIndex++;
                }
            }
            yield return null;
        }
    }
    
    private void Move(out Vector3 targetPosition, out Vector3 newDirection)
    {
        targetPosition = _waypoints[_waypointIndex].transform.position;
        newDirection = (targetPosition - transform.position);
        var movementThisFrame = 5 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }

    // enemy point of view in the direction of the path
    private void RotateIntoMoveDirection(Vector3 newDirection)
    {
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
