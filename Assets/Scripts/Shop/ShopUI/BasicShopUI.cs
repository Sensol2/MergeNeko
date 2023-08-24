using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//���� �⺻���� ���� UI
//��쿡 ���� �ٸ� UI�� ����� �� ������
//�� Ŭ������ ��ӹ޾Ƽ� �������̵�� UI�� �����
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
        //���� �г��� ������ �� �߰��� �����־�� �ϴ� ������ ����
        //ex) �̹� ������ �׸��� ���: ���Ź�ư �����ϱ�
        //�⺻�����δ� ���������, �ʿ��� ��� �߰� ������ �������̵��� ����ϱ�
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
            Debug.Log("���� �����մϴ�.");
            return;
        }
        else
        {
            DataManager.instance.SetGem(gem - item.itemPrice);
            shopUIController.UpdateGemText();
            Debug.Log("���ſϷ�");
        }
    }
}
