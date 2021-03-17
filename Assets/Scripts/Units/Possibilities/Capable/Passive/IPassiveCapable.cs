namespace Units.Possibilities.Capable.Passive
{
    public interface IPassiveCapable
    {
        float Value { get; set; }
        void PassiveAction(Unit selfUnit);
    }
}