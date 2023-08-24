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
        if (DataManager.instance.HasItem(effectData.KEY)) //������ �������� ���
        {
            YesOrNo.SetActive(false);
            if (DataManager.instance.GetEquippedEffect() == effectData.effectType) // ���� �������̸�
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
        Debug.Log("�����Ϸ� " + effectData.effectType);
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
            Debug.Log("���� �����մϴ�.");
            return;
        }
        else if (DataManager.instance.HasItem(effectData.KEY)) //�̹� �������� �������̸�
        {
            Debug.Log("�̹� �������Դϴ�.");
            return;
        }
        else
        {
            DataManager.instance.SetGem(gem - item.itemPrice);
            DataManager.instance.SetItemCount(effectData.KEY, 1);
            shopUIController.UpdateGemText();
            UpdatepanelUI();
            Debug.Log("���ſϷ�");
        }
    }
}