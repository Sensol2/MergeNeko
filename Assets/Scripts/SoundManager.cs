using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    public AudioSource nya;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        nya = GetComponent<AudioSource>();
    }

    public void PlaySound() 
    {
        nya.Play();
    }

}
