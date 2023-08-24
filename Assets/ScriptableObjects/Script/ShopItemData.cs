using UnityEngine;

//가장 기본적인 내용을 담고 있는 Shop data.
//Effect Data 등의 부모가 됨

[CreateAssetMenu(fileName = "ShopItemData", menuName = "Scriptable Object/ShopItemData", order = int.MaxValue)]
public class ShopItemData : ScriptableObject
{
    [SerializeField]
    public Sprite icon;
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}