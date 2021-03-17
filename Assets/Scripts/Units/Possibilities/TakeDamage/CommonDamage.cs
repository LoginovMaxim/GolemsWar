using System;
using System.Linq;
using Gameplay;
using UnityEngine;

namespace Units.Possibilities.TakeDamage
{
    public abstract class CommonDamage : MonoBehaviour, IDamageable
    {
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public virtual void TakeDamage(Unit aggressor, Unit victim, float damage)
        {
            if (victim.Armor.Value >= damage / 2f)
            {
                victim.Armor.ChangeValue(-damage / 2f);
                damage = 0;
            }
            else
            {
                damage -= victim.Armor.Value * 2f;
                victim.Armor.ChangeValue(-victim.Armor.Value);
            }
            
            victim.Health.ChangeValue(-damage);

            if (victim.Health.Value > 0)
                Counterattack(aggressor, victim);
        }
        
        public virtual void TakeDamage(Unit aggressor, Unit victim, float damage, bool onlyHealth)
        {
            if (onlyHealth == false)
            {
                if (victim.Armor.Value >= damage / 2f)
                {
                    victim.Armor.ChangeValue(-damage / 2f);
                    damage = 0;
                }
                else
                {
                    damage -= victim.Armor.Value * 2f;
                    victim.Armor.ChangeValue(-victim.Armor.Value);
                }
            }
            
            victim.Health.ChangeValue(-damage);

            if (victim.Health.Value > 0)
                Counterattack(aggressor, victim);
        }
    
        public virtual void Counterattack(Unit aggressor, Unit victim)
        {
            if (aggressor == victim)
                return;
            
            if (victim.Fraction == _gameManager.CurrentFraction)
                return;
            
            if (victim?.Attackable.AttackDistance >= aggressor?.Attackable.AttackDistance)
            {
                if (victim?.Attackable.BlockedDistance < aggressor?.Attackable.AttackDistance)
                    victim?.Attackable.Counterattack(victim, aggressor.Hex);
            }
        }
    }
}