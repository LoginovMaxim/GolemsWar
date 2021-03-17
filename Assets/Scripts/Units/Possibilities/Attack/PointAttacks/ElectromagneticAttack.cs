using System.Linq;
using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Attack.PointAttacks
{
    public class ElectromagneticAttack : CommonPointAttack
    {
        [SerializeField] private float _electroDamage;
        [SerializeField] private int _countStan;
        
        public override void ProcessAttack(Unit aggressor, Hex attackingHex)
        {
            foreach (var victim in attackingHex.Units.ToList())
            {
                float damage = aggressor.Damage.Value;
                
                if (victim.Armor.Default > 500)
                {
                    damage += _electroDamage;
                    
                    if(victim.Movement != null)
                        victim.Movement.IsStuned = true;
                }
                
                victim.Damageable.TakeDamage(aggressor, victim, damage);
                
                Victims.Add(victim);
            }
        }
    }
}