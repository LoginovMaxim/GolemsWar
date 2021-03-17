using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Attack.PointAttacks
{
    public abstract class CommonPointAttack : CommonAttack
    {
        public List<Unit> Victims = new List<Unit>();
        
        public override void ProcessAttack(Unit aggressor, Hex attackingHex)
        {
            foreach (var victim in attackingHex.Units.ToList())
            {
                victim.Damageable.TakeDamage(aggressor, victim, aggressor.Damage.Value);
                Victims.Add(victim);
            }
        }
        
        public override IEnumerator Attacking(Unit unit, Hex hex)
        {
            yield return StartCoroutine(base.Attacking(unit, hex));
            yield return new WaitForSeconds(1f);
            
            Victims.Clear();
        }
    }
}