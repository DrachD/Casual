using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////////////////////////////////////////////////////////
// class for changing sound volume using a slider
////////////////////////////////////////////////////////
public class SliderAudioSetter : MonoBehaviour
{
    public Slider Slider;
    public FloatVariable Variable;
    private BackgroundMusic _bgMusic;

    private void Awake()
    {
        _bgMusic = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>();
    }

    void Update()
    {
        if (Slider != null && Variable != null)
        {
            Slider.value = Variable.Value;
            _bgMusic.SetVolume(Variable.Value);
        }
    }
}
