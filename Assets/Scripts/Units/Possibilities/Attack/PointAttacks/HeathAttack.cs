using System.Linq;
using Environment.Hex;

namespace Units.Possibilities.Attack.PointAttacks
{
    public class HeathAttack : CommonPointAttack
    {
        public override void ProcessAttack(Unit aggressor, Hex attackingHex)
        {
            foreach (var victim in attackingHex.Units.ToList())
            {
                victim.Damageable.TakeDamage(aggressor, victim, aggressor.Damage.Value, true);
                Victims.Add(victim);
            }
        }
    }
}