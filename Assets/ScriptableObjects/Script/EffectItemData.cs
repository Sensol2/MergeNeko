using UnityEngine;

[CreateAssetMenu(fileName = "EffectItemData", menuName = "Scriptable Object/EffectItemData", order = int.MaxValue)]
public class EffectItemData : ShopItemData
{
    [SerializeField]
    public string KEY;
    public EffectType effectType;
}
