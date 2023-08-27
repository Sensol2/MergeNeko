using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager instance;

    // PlayerPrefs에 사용할 키들
    private const string GEM_KEY = "GemKey";
    private const string EQUIPPED_EFFECT_KEY = "EquippedEffectKey";

    private void Awake()
    {
        if (instance == null)
            instance = this;


        //debug
        //SetGem(1000000);
    }


    // 1. Gem 관련 함수
    public void SetGem(int value)
    {
        PlayerPrefs.SetInt(GEM_KEY, value);
    }

    public int GetGem()
    {
        return PlayerPrefs.GetInt(GEM_KEY, 0); // 기본 값은 0
    }

    // 2. item 보유 여부 저장
    public void SetItemCount(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void PlusItemCount(string key)
    {
        int count = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, count + 1);
    }

    public void MinusItemCount(string key)
    {
        int count = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, count - 1);
    }

    public bool HasItem(string key)
    {
        return PlayerPrefs.GetInt(key) > 0; // 기본 값은 false
    }

    public int GetItemCount(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    // 3. item level 저장
    private string GetItemLevelKey(string key)
    {
        return key + "_Level";
    }

    public void SetItemLevel(string key, int level)
    {
        string levelKey = GetItemLevelKey(key);
        PlayerPrefs.SetInt(levelKey, level);
    }

    public int GetItemLevel(string key)
    {
        string levelKey = GetItemLevelKey(key);
        return PlayerPrefs.GetInt(levelKey, 0); // 기본 레벨은 1로 설정
    }

    public void UpgradeItemLevel(string key)
    {
        int currentLevel = GetItemLevel(key);
        SetItemLevel(key, currentLevel + 1);
    }

    // 4. 장착 효과 저장
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
