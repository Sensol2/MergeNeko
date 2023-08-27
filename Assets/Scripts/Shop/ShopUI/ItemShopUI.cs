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
            Debug.Log("���� �����մϴ�.");
            return;
        }

        else
        {
            DataManager.instance.SetGem(gem - item.itemPrice);
            shopUIController.UpdateGemText();
            CloseShoppanel();
            newItem.SpawnNewItem();
            Debug.Log("���ſϷ�");
        }
    }
}
