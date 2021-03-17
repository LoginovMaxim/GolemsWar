namespace Units.Possibilities.Capable.Auto
{
    public interface IAutoCapable
    {
        float Value { get; set; }
        void AutoAction(Unit unit);
    }
}