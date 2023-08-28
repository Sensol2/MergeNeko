using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 추가

public enum SoundType { NYA, PIT, BOMB }

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    public AudioSource BGMusic;
    public AudioSource soundEffect;

    public AudioClip nya;
    public AudioClip pit;
    public AudioClip bomb;

    // 슬라이더 참조 추가
    public Slider musicVolumeSlider;
    public Slider soundEffectVolumeSlider;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        // 초기 볼륨 설정
        BGMusic.volume = musicVolumeSlider.value * 0.2f; // 0.2f is a scaling factor assuming the max slider value is 5
        soundEffect.volume = soundEffectVolumeSlider.value * 0.2f;

        // 슬라이더 이벤트 추가
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundEffectVolumeSlider.onValueChanged.AddListener(OnSoundEffectVolumeChanged);
    }

    // 음악 볼륨 변경
    public void OnMusicVolumeChanged(float value)
    {
        BGMusic.volume = value * 0.2f;
    }

    // 효과음 볼륨 변경
    public void OnSoundEffectVolumeChanged(float value)
    {
        soundEffect.volume = value * 0.2f;
    }



    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.PIT: soundEffect.PlayOneShot(pit); break;
            case SoundType.NYA: soundEffect.PlayOneShot(nya); break;
            case SoundType.BOMB: soundEffect.PlayOneShot(bomb); break;
        }
    }
}
