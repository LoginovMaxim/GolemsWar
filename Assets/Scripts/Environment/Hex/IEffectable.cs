namespace Environment.Hex
{
    public interface IEffectable
    {
        float Value { get; set; }
        int CountTurn { get; set; }
        
        void DoEffect(Hex hex);
    }
}