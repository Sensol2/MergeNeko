using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public GameObject modal;
    GameItem item;

    public void SetItem(GameItem item)
    {
        this.item = item;
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        int count = DataManager.instance.GetItemCount(item.KEY);
        transform.Find("icon").GetComponent<Image>().sprite = item.icon;
        transform.Find("count").GetComponent<TMP_Text>().text = $"x{count}";
    }

    public void DisplayItemModal()
    {
        var canvas = GameObject.Find("Canvas");
        GameObject obj = Instantiate(modal, canvas.transform, false);
        obj.GetComponent<NewItemModal>().CreateItemDetailModal(this, item);
    }
}
