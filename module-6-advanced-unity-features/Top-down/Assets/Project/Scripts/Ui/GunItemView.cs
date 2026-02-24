using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _indexText;
    
    public void Init(string  gunName, int quickSlotNum)
    {
        _nameText.text = gunName;
        _indexText.text = quickSlotNum.ToString();

    }
}
