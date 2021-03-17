using System.Linq;
using UnityEngine;

namespace Environment.Hex
{
    public class Fire : CommonEffect
    {
        public Fire(Hex hex, float value, int countTurn) : base(value, countTurn)
        {
            hex.CreateVisualEffect(VisualEffectType.Fire);
            
            foreach (var effect in hex.Effects)
            {
                if (effect.GetType() == typeof(Fire))
                    effect.CountTurn = 0;
            }
        }
        
        public override void DoEffect(Hex hex)
        {
            if (IsAliveEffect<Fire>(hex, VisualEffectType.Fire) == false)
                return;
            
            foreach (var anyUnit in hex.Units.ToList())
                anyUnit.Health.ChangeValue(-Value);
        }
    }
}