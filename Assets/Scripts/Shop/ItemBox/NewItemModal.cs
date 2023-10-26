using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewItemModal : MonoBehaviour
{
    ItemInfo itemInfo;
    GameItem item;
    public Image itemIcon;
    public TMP_Text item_name;
    public TMP_Text item_description;

    public TMP_Text upgradeBtn_title;
    public TMP_Text upgradeBtn_description;
    public TMP_Text sellBtn_title;


    public CloseModal closeModal;


    public void CreateItemModal(GameItem item)
    {
        this.item = item;
        itemIcon.sprite = item.icon;

        int currentLevel = DataManager.instance.GetItemLevel(item.KEY);
        item_name.text = item.itemName.GetLocalizedString();
        item_description.text = item.GetItemDescription(currentLevel);
    }

    public void CreateItemDetailModal(ItemInfo info, GameItem item)
    {
        this.itemInfo = info;
        this.item = item;
        itemIcon.sprite = item.icon;

        int currentLevel = DataManager.instance.GetItemLevel(item.KEY);
        item_name.text = item.itemName + $" (Lv.{currentLevel + 1})";
        upgradeBtn_title.text = $"UPGRADE ({item.upgradeCost[currentLevel]}G)";
        item_description.text = item.GetItemDescription(currentLevel);
        upgradeBtn_description.text = item.GetUpgradeDescription(currentLevel);
        sellBtn_title.text = $"SELL ({item.itemPrice}G)";
    }

    public void UpgradeItem()
    {
        int gem = DataManager.instance.GetGem();
        int currentLevel = DataManager.instance.GetItemLevel(item.KEY);

        if (gem < item.upgradeCost[currentLevel])
        {
            Debug.Log("돈이 부족합니다.");
            return;
        }
        else if (currentLevel + 1 >= item.maxLevel)
        {
            Debug.Log("최대 레벨입니다.");
            return;
        }
        else
        {
            DataManager.instance.SetGem(gem - item.upgradeCost[currentLevel]);
            DataManager.instance.UpgradeItemLevel(item.KEY);
            GameObject.Find("GemTextArea").GetComponent<DisplayGem>().UpdateGemText();
            closeModal.DestroyModalUI();
            Debug.Log("구매완료");
        }
    }

    public void SellItem()
    {
        int count = DataManager.instance.GetItemCount(item.KEY);
        if (count > 1)
        {
            int gem = DataManager.instance.GetGem();
            DataManager.instance.SetGem(gem + item.itemPrice);
            DataManager.instance.MinusItemCount(item.KEY);
            itemInfo.UpdateSlotUI();
            GameObject.Find("GemTextArea").GetComponent<DisplayGem>().UpdateGemText();
            closeModal.DestroyModalUI();
            Debug.Log("판매완료");
        }
    }

}
