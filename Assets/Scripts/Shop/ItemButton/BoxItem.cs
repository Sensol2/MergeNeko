using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxItem : MonoBehaviour
{
    private ItemShopUI panel;
    public ShopItemData item;

    private void Start()
    {
        panel = gameObject.GetComponentInParent<ItemShopUI>();
    }

    public void Open()
    {
        panel.item = this.item;
        panel.OpenShoppanel();
    }
}
