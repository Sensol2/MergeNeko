using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseModal : MonoBehaviour
{
    public GameObject modal;

    public void CloseModalUI()
    {
        modal.SetActive(false);
    }
}
