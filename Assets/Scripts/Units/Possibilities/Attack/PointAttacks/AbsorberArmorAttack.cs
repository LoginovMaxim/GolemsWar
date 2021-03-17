using System.Linq;
using Environment.Hex;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Possibilities.Attack.PointAttacks
{
    public class AbsorberArmorAttack : CommonPointAttack
    {
        public event UnityAction<float> OnScrapMetalChanged;
        
        [SerializeField] private int _commonPercent;
        
        private int _damagePercent = 30;
        private int _armorPercent = 30;
        private int _scrapMetalPercent = 40;
        
        public override void ProcessAttack(Unit aggressor, Hex attackingHex)
        {
            foreach (var victim in attackingHex.Units.ToList())
            {
                Victims.Add(victim);
            }

            foreach (var victim in Victims)
            {
                victim.OnDied += Absorb;
            }
            
            foreach (var victim in attackingHex.Units.ToList())
            {
                victim.Damageable.TakeDamage(aggressor, victim, aggressor.Damage.Value);
            }
            
            foreach (var victim in Victims)
            {
                victim.OnDied -= Absorb;
            }
        }

        public void Absorb(Unit unit)
        {
            float commonScrabMetal = (unit.Damage.Default + unit.Armor.Default) * 1.5f;
            commonScrabMetal *= (_commonPercent / 100f);

            float damage = commonScrabMetal * _damagePercent / 100f / 1.5f;
            Self.Damage.ChangeValue(damage, true);
            
            float armor = commonScrabMetal * _armorPercent / 100f / 1.5f;
            Self.Armor.ChangeValue(armor, true);
            
            OnScrapMetalChanged?.Invoke(commonScrabMetal * _scrapMetalPercent / 100f);
        }
    }
}