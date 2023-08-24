using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public ItemEffectType effectType; // ������ ȿ�� ����
    public float[] effectValues; // ���� �� ȿ�� ��
}
