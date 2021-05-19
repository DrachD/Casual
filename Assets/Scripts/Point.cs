using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// the stop point for the enemy, you can change it at your discretion, 
// the stopping point can be either more or less
////////////////////////////////////////////////////////
public class Point : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    public float Delay => delay;
}
