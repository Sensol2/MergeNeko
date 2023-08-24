using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager instance;

    // PlayerPrefs�� ����� Ű��
    private const string GEM_KEY = "GemKey";
    private const string EQUIPPED_EFFECT_KEY = "EquippedEffectKey";

    private void Awake()
    {
        if (instance == null)
            instance = this;


        //debug
        //SetGem(1000000);
    }


    // 1. Gem ���� �Լ�
    public void SetGem(int value)
    {
        PlayerPrefs.SetInt(GEM_KEY, value);
    }

    public int GetGem()
    {
        return PlayerPrefs.GetInt(GEM_KEY, 0); // �⺻ ���� 0
    }

    // 2. item ���� ���� ����
    public void SetItemCount(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void PlusItemCount(string key)
    {
        int count = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, count + 1);
    }

    public bool HasItem(string key)
    {
        return PlayerPrefs.GetInt(key) > 0; // �⺻ ���� false
    }

    public int GetItemCount(string key)
    {
        return PlayerPrefs.GetInt(key);
    }


    public void SetEquippedEffect(EffectType effectType)
    {
        PlayerPrefs.SetInt(EQUIPPED_EFFECT_KEY, (int)effectType);
    }

    public EffectType GetEquippedEffect()
    {
        int equippedItemValue = PlayerPrefs.GetInt(EQUIPPED_EFFECT_KEY, (int)EffectType.None);
        Debug.Log("return: "+ equippedItemValue);
        return (EffectType)equippedItemValue;
    }

}
