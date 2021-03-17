using Environment.Hex;

namespace Units.Possibilities.Capable.Hand
{
    public interface IHandCapable
    {
        float Value { get; set; }
        float Cost { get; set; }
        int Radius { get; set; }
        int RechargeCount { get; set; }
        int RechargePeriod { get; set; }
        bool PendingAction { get; set; }
        
        void ButtonResponse(Unit unit);
            
        void HandAction(Unit unit, Hex hex);
        
        void HandAction(Unit selfUnit, Unit enemyUnit);

        void Recharge(Unit unit);
    }
}