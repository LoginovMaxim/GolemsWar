using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Units.UnitStats
{
    [Serializable]
    public class UnitStats
    {
        public event UnityAction<float> OnChangedValue;
        public event UnityAction OnNuliffiedValue;

        public float Value;
        
        [HideInInspector]
        public float Limit;
        
        [HideInInspector]
        public float Default;

        public virtual void Init()
        {
            Default = Value;
        }

        public void ChangeValue(float delta)
        {
            Value += delta;
            
            if (Value > Limit)
                Value = Limit;
            else if (Value <= 0)
                Value = 0;

            OnChangedValue?.Invoke(Value);
            
            if (Value <= 0)
                OnNuliffiedValue?.Invoke();
        }
        
        public void ChangeValue(float delta, bool changeLimit)
        {
            Value += delta;
            
            if (changeLimit == false)
            {
                if (Value > Limit)
                    Value = Limit;
            }
            else
            {
                if (Value > Limit)
                    Limit = Value;
            }

            if (Value <= 0)
                Value = 0;
            
            OnChangedValue?.Invoke(Value);
            
            if (Value <= 0)
                OnNuliffiedValue?.Invoke();
        }

        public void ResetValue() =>
            Value = Default;
    }
}