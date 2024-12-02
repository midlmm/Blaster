using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RechargeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textAmmo;
    [SerializeField] private Slider _sliderRecharge;

    private IToolitemable _currentToolitem;

    public void OnChangeToolitem(IToolitemable toolitem)
    {
        if(_currentToolitem != null)
        {
            _currentToolitem.OnChangeAmmo -= DisplayCountAmmo;
            _currentToolitem.OnChangeTimeRecharge -= DispayValueSlider;
            _currentToolitem.OnChangeStateRecharge -= DispayActiveSlider;
        }

        toolitem.OnChangeAmmo += DisplayCountAmmo;
        toolitem.OnChangeTimeRecharge += DispayValueSlider;
        toolitem.OnChangeStateRecharge += DispayActiveSlider;

        _currentToolitem = toolitem;
    }

    private void DisplayCountAmmo(int count)
    {
        _textAmmo.text = count.ToString();
    }

    private void DispayValueSlider(float value)
    {
        _sliderRecharge.value = _sliderRecharge.maxValue - value;
    }

    private void DispayActiveSlider(bool active, float maxValue)
    {
        _sliderRecharge.gameObject.SetActive(active);
        if (!active)
            return;
        _sliderRecharge.value = 0;
        _sliderRecharge.maxValue = maxValue;
    }
} 
