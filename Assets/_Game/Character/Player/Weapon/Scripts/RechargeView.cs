using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RechargeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textAmmo;
    [SerializeField] private Slider _sliderRecharge;

    public void DisplayCountAmmo(int count)
    {
        _textAmmo.text = count.ToString();
    }

    public void DispayValueSlider(float value)
    {
        _sliderRecharge.value = value;
    }

    public void DispaySetActiveSlider(bool active, float time)
    {
        _sliderRecharge.gameObject.SetActive(active);
        if (!active) return;
        _sliderRecharge.value = 0;
        _sliderRecharge.maxValue = time;
    }
} 
