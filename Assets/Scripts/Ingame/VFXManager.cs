using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    None,
    LovelyHeart,
    Starlight,
    // 추가적인 이펙트는 여기에
}

public class VFXManager : MonoBehaviour
{
    public GameObject[] effects;


	public static VFXManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void GenerateEffect(Vector3 position)
    {
        EffectType index = DataManager.instance.GetEquippedEffect();
        Debug.Log(index);
        Instantiate(effects[(int)index], position, Quaternion.identity);
    }

    private void Start()
	{

	}


}
