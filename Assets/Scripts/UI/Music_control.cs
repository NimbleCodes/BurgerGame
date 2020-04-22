using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_control : MonoBehaviour
{
    public Slider backVolume;//Slider의 value값
    public AudioSource audio;//소리를 조절해줄 변수

    public float backVol = 1f;


    void Start()
    {
     	backVol = PlayerPrefs.GetFloat("backvol",1f);//처음 값이 0이어서 소리가 들리지 않는걸 방지
     	backVolume.value = backVol;
     	audio.volume = backVolume.value;   
    }


    
    void Update()
    {
    	SoundSlider();    
    }

    public void SoundSlider()
    {
    	audio.volume = backVolume.value;

    	backVol = backVolume.value;
    	PlayerPrefs.SetFloat("backvol",backVol);
    }
}
