using System.Collections;
using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Attack
{
    public abstract class CommonAttack : MonoBehaviour, IAttackable
    {
        [SerializeField] private int _attackDistance;
        [SerializeField] private int _blockedDistance;
        [SerializeField] private int _countAttackPoints;
        [SerializeField] private GameObject VFXParticles;
        
        private int _initialCountAttackPoints;

        protected Unit Self;
        
        protected void Start()
        {
            _initialCountAttackPoints = _countAttackPoints;

            Self = GetComponent<Unit>();
        }
        
        public int AttackDistance
        {
            get => _attackDistance;
            set => _attackDistance = value;
        }

        public int BlockedDistance
        {
            get => _blockedDistance;
            set => _blockedDistance = value;
        }

        public int CountAttackPoints
        {
            get => _countAttackPoints;
            set => _countAttackPoints = value;
        }

        public virtual void FindTarget(Unit unit)
        {
            unit.Hex.FindAttackUnits(unit);
        }

        public virtual void Attack(Unit aggressor, Hex attackingHex)
        {
            if (attackingHex.Points[1] <= 0)
                return;

            CreateVFXAttack(attackingHex);
            
            StartCoroutine(Attacking(aggressor, attackingHex));
            CountAttackPoints--;
        }

        public virtual void Counterattack(Unit aggressor, Hex attackingHex)
        {
            CreateVFXAttack(attackingHex);
            
            StartCoroutine(Attacking(aggressor, attackingHex));
            CountAttackPoints--;
        }

        public void CreateVFXAttack(Hex attackingHex)
        {
            if (VFXParticles != null)
                Instantiate(VFXParticles, attackingHex.transform);
        }

        public void ResetPoints(Unit unit) =>
            _countAttackPoints = _initialCountAttackPoints;

        public abstract void ProcessAttack(Unit aggressor, Hex attackingHex);
        
        public virtual IEnumerator Attacking(Unit unit, Hex hex)
        {
            Quaternion prevRotation = unit.transform.rotation;
            Quaternion targetRotation =
                Quaternion.LookRotation(hex.transform.position - unit.transform.position);
            
            float t = 0;
            while (t < 1)
            {
                unit.transform.rotation = Quaternion.Lerp(prevRotation, targetRotation, 6f * t);
                t += Time.deltaTime;
                yield return null;
            }
            
            if (unit.Animators.Count > 0)
                foreach (var animator in unit.Animators)
                {
                    animator.SetTrigger("Attack");
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                }
            
            yield return new WaitForSeconds(0.1f);
            
            ProcessAttack(unit, hex);
        }
    }
}