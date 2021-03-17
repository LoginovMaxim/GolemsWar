using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Attack.AreaAttacks
{
    public abstract class CommonAreaAttack : CommonAttack
    {
        public float[] CircleDamages;

        public List<Unit> Victims = new List<Unit>();
        public List<Hex> AttackedHexes = new List<Hex>();
        
        public override void ProcessAttack(Unit aggressor, Hex attackingHex)
        {
            List<Hex> hexes = new List<Hex>();
            List<Hex> newHexes = new List<Hex>();

            int index = 0;
            
            hexes.Add(attackingHex);
            attackingHex.Checked = true;

            foreach (var victim in attackingHex.Units.ToList())
            {
                victim.Damageable.TakeDamage(aggressor, victim, aggressor.Damage.Value);
                
                if (victim.Health.Value > 0)
                    Victims.Add(victim);
            }
            
            AttackedHexes.Add(attackingHex);

            int attackingArea = CircleDamages.Length;
            while (attackingArea > 0)
            {
                foreach (var hex in hexes)
                {
                    foreach (var neighbor in hex.NeighborHexes)
                    {
                        if (neighbor.Checked)
                            continue;
                        
                        if (neighbor.Playable == false)
                            continue;
                        
                        foreach (var victim in neighbor.Units.ToList())
                        {
                            if (Victims.Contains(victim))
                                continue;
                            
                            victim.Damageable.TakeDamage(aggressor, victim, CircleDamages[index]);
                            
                            if (victim.Health.Value > 0)
                                Victims.Add(victim);
                        }

                        neighbor.Checked = true;
                        
                        newHexes.Add(neighbor);
                        AttackedHexes.Add(neighbor);
                    }
                }

                hexes.Clear();
                
                foreach (var newHex in newHexes)
                    hexes.Add(newHex);
                
                newHexes.Clear();

                attackingArea--;
                index++;
            }

            foreach (var attacked in AttackedHexes)
            {
                attacked.Checked = false;
            }
        }
        
        public override IEnumerator Attacking(Unit unit, Hex hex)
        {
            yield return StartCoroutine(base.Attacking(unit, hex));
            yield return new WaitForSeconds(1f);
            
            Victims.Clear();
            AttackedHexes.Clear();
        }
    }
}