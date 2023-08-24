using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotGenerator : MonoBehaviour
{
    [Header("Items")]
    public List<GameItem> items = new List<GameItem>();

    public GameObject itemSlot_Unknown;
    public GameObject itemSlot_Normal;
    public GameObject itemSlot_Epic;
    public GameObject itemSlot_Legendary;

    private void Start()
    {
        GenerateItemSlots();
    }

    

    void GenerateItemSlots()
    {
        foreach (GameItem item in items)
        {
            int count = DataManager.instance.GetItemCount(item.KEY);
            if (count > 0)
            {
                GameObject slot;
                switch (item.itemRank)
                {
                    case ItemRank.NORMAL:
                        slot = Instantiate(itemSlot_Normal, this.transform, false);
                        break;
                    case ItemRank.EPIC:
                        slot = Instantiate(itemSlot_Epic, this.transform, false);
                        break;
                    case ItemRank.LEGENDARY:
                        slot = Instantiate(itemSlot_Legendary, this.transform, false);
                        break;
                    default:
                        throw new System.Exception();
                }
                slot.transform.Find("icon").GetComponent<Image>().sprite = item.icon;
                slot.transform.Find("count").GetComponent<TMP_Text>().text = $"x{count}";
            }
            else
            {
                Instantiate(itemSlot_Unknown, this.transform, false);
            }

            
        }
    }
}
