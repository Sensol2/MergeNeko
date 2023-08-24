using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : MonoBehaviour
{
    private EffectShopUI panel;
    public EffectItemData item;

    private void Start()
    {
        panel = gameObject.GetComponentInParent<EffectShopUI>();    
    }

    public void Open()
    {
        panel.item = this.item;
        panel.effectData = this.item;
        panel.OpenShoppanel();
    }
}
