using System;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI
{
    public class SkillDetailUI : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private float _multiplier = 1f;
        [SerializeField] private Image _icon;
        private float _currentValue;
        private float _maxValue;

        private void Start()
        {
            _fill.fillAmount = 0;
            _currentValue = 0;
            _maxValue = 0;
        }

        private void Update()
        {
            if (_currentValue > 0)
            {
                _currentValue -= Time.deltaTime * _multiplier;
                _fill.fillAmount = _currentValue / _maxValue;
            }
        }
        
        public void Initialize(float maxValue)
        {
            _maxValue = maxValue;
            _currentValue = maxValue;
            _fill.fillAmount = 1;
        }
        
        public void UpdateValue(float value)
        {
            _currentValue = Mathf.Clamp(_currentValue + value, 0, _maxValue);
            _fill.fillAmount = _currentValue / _maxValue;
        }
        
        public void SetValue(float value)
        {
            _currentValue = Mathf.Clamp(value, 0, _maxValue);
            _fill.fillAmount = _currentValue / _maxValue;
        }
        
        public void SetMaxValue(float value)
        {
            _maxValue = value;
            _currentValue = value;
            _fill.fillAmount = 1;
        }
        
        public void SetMultiplier(float value)
        {
            _multiplier = value;
        }
        
        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
        
        public Sprite GetIcon()
        {
            return _icon.sprite;
        }
    }
}