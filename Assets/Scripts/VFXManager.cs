using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject heartEffect;

	public static VFXManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void GenerateEffect(Vector3 position)
    {
        Instantiate(heartEffect, position, Quaternion.identity);
    }

    private void Start()
	{

	}


}
