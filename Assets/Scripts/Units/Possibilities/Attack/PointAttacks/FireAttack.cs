using Environment.Hex;
using UnityEngine;

namespace Units.Possibilities.Attack.PointAttacks
{
    public class FireAttack : CommonPointAttack
    {
        [SerializeField] private float _fireDamage;
        [SerializeField] private  int _countTurn;

        public override void ProcessAttack(Unit aggressor, Hex attackingHex)
        {
            base.ProcessAttack(aggressor, attackingHex);
            attackingHex.Effects.Add(new Fire(attackingHex, _fireDamage, _countTurn));
        }
    }
}