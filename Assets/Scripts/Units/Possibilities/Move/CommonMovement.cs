using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Move
{
    public abstract class CommonMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private bool _isAir;
        [SerializeField] private float _movementSpeed;

        [SerializeField] private int _countMovePoints;

        private bool _isStuned;
        private int _initialCountMovePoints;
        
        protected void Start()
        {
            _initialCountMovePoints = _countMovePoints;
        }

        public bool IsAir
        {
            get => _isAir;
            set => _isAir = value;
        }

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }

        public int CountMovePoints
        {
            get => _countMovePoints;
            set => _countMovePoints = value;
        }

        public bool IsStuned
        {
            get => _isStuned;
            set => _isStuned = value;
        }

        public abstract void Move(Unit unit, Hex targetHex);

        public abstract void FindPath(Unit unit);

        public void ResetPoints(Unit unit)
        {
            if (_isStuned == false)
                _countMovePoints = _initialCountMovePoints;
            else
                _countMovePoints = 0;
        }
    }
}