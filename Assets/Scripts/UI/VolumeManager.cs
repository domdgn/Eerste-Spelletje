using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;
    [SerializeField] public float volumeSliderValue = 1f;
    private float startVolume = 1f;

    private void Start()
    {
        AudioListener.volume = startVolume * volumeSliderValue;
    }
    public void OnValueChanged()
    {
        volumeSliderValue = volumeSlider.value;
        AudioListener.volume = startVolume * volumeSliderValue; 
        Debug.Log(AudioListener.volume);
    }
}
