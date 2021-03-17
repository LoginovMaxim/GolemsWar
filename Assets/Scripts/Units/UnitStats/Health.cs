using System;

namespace Units.UnitStats
{
    [Serializable]
    public class Health : UnitStats
    {
        public override void Init()
        {
            base.Init();
            
            Limit = Default * 1.33f;
        }
    }
}
