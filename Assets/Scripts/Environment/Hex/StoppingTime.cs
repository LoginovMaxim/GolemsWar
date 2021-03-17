using System.Linq;

namespace Environment.Hex
{
    public class StoppingTime : CommonEffect
    {
        public StoppingTime(Hex hex, float value, int countTurn) : base(value, countTurn)
        {
            hex.CreateVisualEffect(VisualEffectType.Chrono);
            
            foreach (var effect in hex.Effects)
            {
                if (effect.GetType() == typeof(StoppingTime))
                    effect.CountTurn = 0;
            }
        }

        public override void DoEffect(Hex hex)
        {
            if (IsAliveEffect<StoppingTime>(hex, VisualEffectType.Chrono) == false)
                return;
            
            foreach (var anyUnit in hex.Units.ToList())
                anyUnit.Health.ChangeValue(-Value);
        }
    }
}