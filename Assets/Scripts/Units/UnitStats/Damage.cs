using System;

namespace Units.UnitStats
{
    [Serializable]
    public class Damage : UnitStats
    {
        public override void Init()
        {
            base.Init();
            
            if (Default == 0)
                Limit = 100;
            else if (Default > 0 && Default <= 100)
                Limit = Default * 2f;
            else if (Default > 100 && Default <= 200)
                Limit = Default * 1.9f;
            else if (Default > 200 && Default <= 300)
                Limit = Default * 1.8f;
            else if (Default > 300 && Default <= 400)
                Limit = Default * 1.7f;
            else if (Default > 400 && Default <= 500)
                Limit = Default * 1.6f;
            else if (Default > 500 && Default <= 600)
                Limit = Default * 1.5f;
            else if (Default > 600 && Default <= 700)
                Limit = Default * 1.4f;
            else if (Default > 700 && Default <= 800)
                Limit = Default * 1.3f;
            else if (Default > 800 && Default <= 900)
                Limit = Default * 1.2f;
            else if (Default > 900)
                Limit = Default * 1.1f;
        }
    }
}
