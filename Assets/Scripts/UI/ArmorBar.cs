using UnityEngine;

namespace UI
{
    public class ArmorBar : ValueBar
    {
        private void OnEnable()
        {
            Unit.Armor.OnChangedValue += ChangeChangedValue;
            Unit.OnDied += HideBar;
        }
        
        private void Start()
        {
            Slider.value = Unit.Armor.Default;
        }
        
        public void ChangeChangedValue(float value)
        {
            Slider.value = value / Unit.Armor.Default;
        }

        private void OnDisable()
        {
            Unit.Armor.OnChangedValue -= ChangeChangedValue;
            Unit.OnDied -= HideBar;
        }
    }
}