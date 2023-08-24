using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewItemModal : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text item_name;
    public TMP_Text item_description;

    public void UpdateModalUI(GameItem item)
    {
        itemIcon.sprite = item.icon;
        itemIcon.rectTransform.sizeDelta = new Vector2(item.icon.rect.width, item.icon.rect.height);
        item_name.text = item.itemName;
        item_description.text = item.itemDescription;
    }
}
