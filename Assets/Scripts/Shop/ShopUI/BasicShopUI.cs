using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//가장 기본적인 상점 UI
//경우에 따라 다른 UI를 띄워야 될 때에는
//이 클래스를 상속받아서 오버라이드된 UI를 띄워줌
public class BasicShopUI : MonoBehaviour
{
    public DisplayGem shopUIController;
    public GameObject canvas;

    public GameObject panel;


    public ShopItemData item;

    public Image itemIcon;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text priceText;



    [SerializeField] private float duration = 1f; // Animation duration
    [SerializeField] private Vector3 targetPosition; // The position where the banner will move to
    [SerializeField] private float delay = 2f; // Delay before the banner starts moving back

    private void Awake()
    {
        DOTween.Init();
    }

    public virtual void UpdateUI()
    { 
        //상점 패널을 열었을 때 추가로 취해주어야 하는 동작을 정의
        //ex) 이미 구매한 항목의 경우: 구매버튼 삭제하기
        //기본적으로는 비어있으나, 필요한 경우 추가 동작을 오버라이드해 사용하기
    }

    public void OpenShoppanel()
    {
        itemIcon.sprite = item.icon;
        itemIcon.rectTransform.sizeDelta = new Vector2(item.icon.rect.width, item.icon.rect.height);

        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDescription;
        priceText.text = item.itemPrice.ToString();

        UpdateUI();

        panel.SetActive(true);
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutExpo);
    }

    public void CloseShoppanel()
    {
        panel.SetActive(false);
    }

    public virtual void BuyItem()
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
            Debug.Log("구매완료");
        }
    }
}
