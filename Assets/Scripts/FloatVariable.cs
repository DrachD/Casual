using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// here you can store the state of the sound volume as I do
[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    public float Value;

    public void SetValue(float value)
    {
        Value = value;
    }
}
