using Environment.Hex;

namespace Units.Possibilities.Attack
{
    public interface IAttackable
    {
        int AttackDistance { get; set; }
        int BlockedDistance { get; set; }
        int CountAttackPoints { get; set; }
        
        void FindTarget(Unit unit);
        void Attack(Unit aggressor, Hex attackingHex);
        void Counterattack(Unit aggressor, Hex attackingHex);
        void ResetPoints(Unit unit);
    }
}