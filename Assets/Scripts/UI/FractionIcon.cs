using Units;
using Units.UnitStats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FractionIcon : MonoBehaviour
    {
        public Unit Unit;
        public Image Icon;

        private void Start()
        {
            if (Unit.Fraction % 2 == 0)
            {
                Icon.color = Color.green * 0.8f;
            }
            else if (Unit.Fraction % 2 != 0)
            {
                Icon.color = Color.red * 0.8f;
            }
            else if (Unit.Fraction == 0)
            {
                Icon.color = Color.gray * 0.8f;
            }
        }
    }
}
