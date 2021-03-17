using UnityEngine;

namespace Units.Possibilities.Capable.Passive
{
    public abstract class CommonPassiveCapable : MonoBehaviour, IPassiveCapable
    {
        [SerializeField] private float _value; 
        
        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public abstract void PassiveAction(Unit selfUnit);
    }
}