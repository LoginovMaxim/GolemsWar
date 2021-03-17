using System;

namespace UI
{
    public class HealthBar : ValueBar
    {
        private void OnEnable()
        {
            Unit.Health.OnChangedValue += ChangeChangedValue;
            Unit.OnDied += HideBar;
        }

        private void Start()
        {
            if (Unit.Health.Value == 0)
                Slider.gameObject.SetActive(false);
            
            Slider.value = Unit.Health.Default;
        }

        public void ChangeChangedValue(float value)
        {
            Slider.value = value / Unit.Health.Default;
        }

        private void OnDisable()
        {
            Unit.Health.OnChangedValue -= ChangeChangedValue;
            Unit.OnDied -= HideBar;
        }
    }
}