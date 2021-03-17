namespace Environment.Hex
{
    public abstract class CommonEffect : IEffectable
    {
        public float Value { get; set; }
        public int CountTurn { get; set; }

        public CommonEffect(float value, int countTurn)
        {
            Value = value;
            CountTurn = countTurn;
        }
        
        public abstract void DoEffect(Hex hex);

        public bool IsAliveEffect<T>(Hex hex, VisualEffectType visualEffectType)
        {
            CountTurn--;

            if (CountTurn <= 0)
            {
                bool isAliveEffect = false;
                
                hex.Effects.Remove(this);
                foreach (var effect in hex.Effects)
                {
                    if (effect.GetType() == typeof(T))
                    {
                        isAliveEffect = true;
                        break;
                    }
                }
                
                if (isAliveEffect == false)
                    hex.DestroyVisualEffect(visualEffectType);
                
                return false;
            }
            
            return true;
        }
    }
}