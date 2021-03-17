using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Capable.Hand
{
    public abstract class CommonHandCapable : MonoBehaviour, IHandCapable
    {
        [SerializeField] private float _value;
        [SerializeField] private float _cost;
        
        [SerializeField] private int _radius;
        [SerializeField] private int _rechargeCount;
        [SerializeField] private int _rechargePeriod;
        
        private bool _pendingAction;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public float Cost
        {
            get => _cost;
            set => _cost = value;
        }

        public int Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public int RechargeCount
        {
            get => _rechargeCount;
            set => _rechargeCount = value;
        }

        public int RechargePeriod
        {
            get => _rechargePeriod;
            set => _rechargePeriod = value;
        }

        public bool PendingAction
        {
            get => _pendingAction;
            set => _pendingAction = value;
        }
        
        public abstract void ButtonResponse(Unit unit);
        
        public abstract void HandAction(Unit unit, Hex hex);

        public abstract void HandAction(Unit selfUnit, Unit enemyUnit);
        
        public void Recharge(Unit unit)
        {
            if (_rechargeCount == 0)
                return;

            if (_rechargeCount > 0)
                _rechargeCount--;
        }
    }
}