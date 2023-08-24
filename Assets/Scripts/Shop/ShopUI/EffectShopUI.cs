using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class EffectShopUI : BasicShopUI
{
    public EffectItemData effectData;

    public GameObject YesOrNo;
    public GameObject EquipButton;
    public GameObject UnequipButton;

    public override void UpdateUI()
    {
        UpdatepanelUI();
    }

    void UpdatepanelUI()
    {
        if (DataManager.instance.HasItem(effectData.KEY)) //아이템 보유중인 경우
        {
            YesOrNo.SetActive(false);
            if (DataManager.instance.GetEquippedEffect() == effectData.effectType) // 현재 장착중이면
            {
                EquipButton.SetActive(false);
                UnequipButton.SetActive(true);
            }
            else
            {
                EquipButton.SetActive(true);
                UnequipButton.SetActive(false);
            }
        }
        else
        {
            YesOrNo.SetActive(true);
            EquipButton.SetActive(false);
            UnequipButton.SetActive(false);
        }
    }

    public void EquipEffect()
    {
        Debug.Log("장착완료 " + effectData.effectType);
        DataManager.instance.SetEquippedEffect(effectData.effectType);
        UpdatepanelUI();
    }

    public void UnequipEffect()
    {
        DataManager.instance.SetEquippedEffect(EffectType.None);
        UpdatepanelUI();
    }

    public override void BuyItem()
    {
        int gem = DataManager.instance.GetGem();
        if (gem < item.itemPrice)
        {
            Debug.Log("돈이 부족합니다.");
            return;
        }
        else if (DataManager.instance.HasItem(effectData.KEY)) //이미 보유중인 아이템이면
        {
            Debug.Log("이미 보유중입니다.");
            return;
        }
        else
        {
            DataManager.instance.SetGem(gem - item.itemPrice);
            DataManager.instance.SetItemCount(effectData.KEY, 1);
            shopUIController.UpdateGemText();
            UpdatepanelUI();
            Debug.Log("구매완료");
        }
    }
}