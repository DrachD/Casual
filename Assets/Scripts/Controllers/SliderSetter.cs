using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////////////////////////////////////////////////////////
// a class for changing the volume of effects using a slider
// Currently unused!
////////////////////////////////////////////////////////
public class SliderSetter : MonoBehaviour
{
    public Slider Slider;
    public FloatVariable Variable;

    void Update()
    {
        if (Slider != null && Variable != null)
        {
            Slider.value = Variable.Value;
        }
    }
}
