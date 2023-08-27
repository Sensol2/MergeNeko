using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject panel;
    public GameObject canvas;

    [Header("Items")]
    public List<GameItem> items = new List<GameItem>();

    [Header("Rank Probabilities")]
    [Range(0, 1)] public float normalProbability = 0.7f;
    [Range(0, 1)] public float epicProbability = 0.2f;
    // Legendary probability is whatever remains from 1 after subtracting the others.

    public void SpawnNewItem()
    {
        GameItem selected = DrawRandomItem();

        //Data 저장
        DataManager.instance.PlusItemCount(selected.KEY);

        //모달창 생성
        GameObject obj = Instantiate(panel, canvas.transform, false);
        NewItemModal newItemModal = obj.GetComponent<NewItemModal>();
        newItemModal.CreateItemModal(selected);
        obj.transform.localScale = Vector3.zero;
        obj.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutExpo);
    }

    private GameItem DrawRandomItem()
    {
        float draw = Random.value;

        List<GameItem> pool;
        if (draw < normalProbability) // NORMAL 
        {
            pool = items.FindAll(i => i.itemRank == ItemRank.NORMAL);
        }
        else if (draw < normalProbability + epicProbability) // EPIC
        {
            pool = items.FindAll(i => i.itemRank == ItemRank.EPIC);
        }
        else //LEGENDARY
        {
            pool = items.FindAll(i => i.itemRank == ItemRank.LEGENDARY);
        }

        return pool[Random.Range(0, pool.Count)];
    }
}
