using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGem : MonoBehaviour
{
    public TextMeshProUGUI gemText;

    void Start()
    {
        UpdateGemText();
    }

    public void UpdateGemText()
    {
        gemText.text = DataManager.instance.GetGem().ToString() + " GEM";
    }
}
