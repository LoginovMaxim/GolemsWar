using UnityEngine;

namespace Units.Possibilities.Capable.Auto
{
    public abstract class CommonAutoCapable : MonoBehaviour, IAutoCapable
    {
        [SerializeField] private float _value;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public abstract void AutoAction(Unit unit);
    }
}