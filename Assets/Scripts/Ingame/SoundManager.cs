using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �߰�

public enum SoundType { NYA, PIT, BOMB }

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    public AudioSource BGMusic;
    public AudioSource soundEffect;

    public AudioClip nya;
    public AudioClip pit;
    public AudioClip bomb;

    // �����̴� ���� �߰�
    public Slider musicVolumeSlider;
    public Slider soundEffectVolumeSlider;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        // �ʱ� ���� ����
        BGMusic.volume = musicVolumeSlider.value * 0.2f; // 0.2f is a scaling factor assuming the max slider value is 5
        soundEffect.volume = soundEffectVolumeSlider.value * 0.2f;

        // �����̴� �̺�Ʈ �߰�
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundEffectVolumeSlider.onValueChanged.AddListener(OnSoundEffectVolumeChanged);
    }

    // ���� ���� ����
    public void OnMusicVolumeChanged(float value)
    {
        BGMusic.volume = value * 0.2f;
    }

    // ȿ���� ���� ����
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
