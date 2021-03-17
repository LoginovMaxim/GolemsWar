using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Attack.AreaAttacks
{
    public class FireAreaAttack : CommonAreaAttack
    {
        [SerializeField] private float _fireDamage;
        [SerializeField] private  int _countTurn;
        
        public override void ProcessAttack(Unit aggressor, Hex attackingHex)
        {
            base.ProcessAttack(aggressor, attackingHex);

            foreach (var hex in AttackedHexes)
            {
                if (hex.Playable)
                    hex.Effects.Add(new Fire(hex, _fireDamage, _countTurn));
            }
        }
    }
}