using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemShopUI : BasicShopUI
{
    public ItemGenerator newItem;
    public override void UpdateUI()
    {
    }

    public override void BuyItem()
    {
        int gem = DataManager.instance.GetGem();
        if (gem < item.itemPrice)
        {
            Debug.Log("돈이 부족합니다.");
            return;
        }

        else
        {
            DataManager.instance.SetGem(gem - item.itemPrice);
            shopUIController.UpdateGemText();
            CloseShoppanel();
            newItem.SpawnNewItem();
            Debug.Log("구매완료");
        }
    }
}
