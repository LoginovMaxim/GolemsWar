using System;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ValueBar : MonoBehaviour
    {
        public Unit Unit;
        public Slider Slider;

        public virtual void HideBar(Unit unit)
        {
            Slider.gameObject.SetActive(false);
        }
    }
}
