using UnityEngine;

//���� �⺻���� ������ ��� �ִ� Shop data.
//Effect Data ���� �θ� ��

[CreateAssetMenu(fileName = "ShopItemData", menuName = "Scriptable Object/ShopItemData", order = int.MaxValue)]
public class ShopItemData : ScriptableObject
{
    [SerializeField]
    public Sprite icon;
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}