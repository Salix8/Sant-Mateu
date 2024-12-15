using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImagenTextoInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textInfo;
    [SerializeField] private GameObject background;

    void Update()
    {
        if (textInfo.text != "")
            background.SetActive(true);
        else 
            background.SetActive(false);
    }
}
