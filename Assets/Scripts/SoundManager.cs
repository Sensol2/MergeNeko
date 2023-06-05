using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    public AudioSource BGMusic;
    public AudioSource soundEffect;
    public AudioClip nya;
    public AudioClip pit;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }

    public void PlayMergeSound() 
    {
        soundEffect.PlayOneShot(nya);
    }

    public void PlayPitSound()
    {
        soundEffect.PlayOneShot(pit);
    }

}
