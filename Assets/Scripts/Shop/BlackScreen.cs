using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlackScreen : MonoBehaviour, IPointerClickHandler
{
    public ItemShopUI itemShoppanel;
    public EffectShopUI effectShoppanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        itemShoppanel.CloseShoppanel();
        effectShoppanel.CloseShoppanel();
    }
}



