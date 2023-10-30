using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public enum ItemEffectType
{
    IncreaseScore,
    IncreaseGold,
    ExtendFeverTime,
    IncreaseGameOverLimit,
    MergeChance,
    Explosion
}

public enum ItemRank
{ 
    NORMAL,
    EPIC,
    LEGENDARY
}

[CreateAssetMenu(fileName = "GameItem", menuName = "Scriptable Object/GameItem", order = int.MaxValue)]
public class GameItem : ShopItemData
{
    [SerializeField]
    public string KEY;
    public ItemRank itemRank;
    public int level;
    public int maxLevel;
    public int[] upgradeCost;
    public float[] effectValues; // 레벨 별 효과 값
    public LocalizedString estimated_value = new LocalizedString { TableReference = "ScriptsTable", TableEntryReference = "estimated_value" };

    public string GetItemDescription(int level)
    {
        if (level >= 0 && level < effectValues.Length)
        {
            float value = effectValues[level];
            if (value % 1 == 0) // 정수인 경우
                return string.Format(itemDescription.GetLocalizedString(), value.ToString("0"));
            else // 소수점인 경우
                return string.Format(itemDescription.GetLocalizedString(), value.ToString("0.0"));

        }
        else
            return "Invalid level.";
    }

    public string GetUpgradeDescription(int level)
    {
        if (level >= 0 && level + 1 < effectValues.Length)
        {
            float value = effectValues[level];
            float nextValue = effectValues[level+1];
            if (value % 1 == 0) // 정수인 경우
                return string.Format("Next : {0} > {1}", value.ToString("0"), nextValue.ToString("0"));
            else // 소수점인 경우
                return string.Format("Next : {0} > {1}", value.ToString("0.0"), nextValue.ToString("0.0"));
        }
        else
            return "MAX LEVEL";
    }
}
