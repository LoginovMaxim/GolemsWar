using Environment.Hex;

namespace Units.Possibilities.Move
{
    public interface IMovement
    {
        bool IsAir { get; set; }
        float MovementSpeed { get; set; }
        int CountMovePoints { get; set; }
        bool IsStuned { get; set; }
        
        void Move(Unit unit, Hex targetHex);
        void FindPath(Unit unit);
        void ResetPoints(Unit unit);
    }
}
