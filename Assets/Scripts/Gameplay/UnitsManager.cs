using System.Collections.Generic;
using System.Linq;
using Units;
using Units.UnitStats;
using UnityEngine;

namespace Gameplay
{
    public class UnitsManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        public List<Unit> AllyUnits;
        public List<Unit> EnemyUnits;
        
        public void Startup()
        {
            AllyUnits = new List<Unit>();
            EnemyUnits = new List<Unit>();

            List<Unit> units = new List<Unit>();

            units = FindObjectsOfType<Unit>().ToList();

            foreach (var unit in units)
            {
                if (unit.Fraction % 2 == 0)
                    AllyUnits.Add(unit);
                else if (unit.Fraction % 2 != 0)
                    EnemyUnits.Add(unit);
            }
            
            Status = ManagerStatus.Started;
        }
    }
}