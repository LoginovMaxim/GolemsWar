using System;
using Environment;
using Helpers;
using InputController;
using UI;
using Units;
using Units.UnitStats;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public FlyCamera FlyCamera;
        public Controller Controller;
        public Managers Managers;
        public EndTurnButton EndTurnButton;
        public GameInfo GameInfo;
        public MapManager MapManager;
        public int CurrentFraction;

        public int CountTurn;

        private int indexSelectedUnit = 0;
        
        private void OnEnable()
        {
            if (EndTurnButton == null)
                EndTurnButton = FindObjectOfType<EndTurnButton>();

            EndTurnButton.OnPressedEndTurnButton += PassTurn;
        }

        private void Start()
        {
            GameInfo = FindObjectOfType<GameInfo>();
        }

        private void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.E))
            {
                Controller.DeselectAll();
                if (CurrentFraction == Fraction.Ally)
                {
                    if (indexSelectedUnit + 1 == Managers.UnitsManager.AllyUnits.Count)
                        indexSelectedUnit = 0;
                    
                    for (int i = indexSelectedUnit + 1; i < Managers.UnitsManager.AllyUnits.Count; i++)
                    {
                        Unit unit = Managers.UnitsManager.AllyUnits[i];

                        if (unit.Movement?.CountMovePoints > 0 ||
                            unit.Attackable?.CountAttackPoints > 0 ||
                            unit.HandCapable?.RechargeCount == 0)
                        {
                            indexSelectedUnit = i;
                            break;
                        }
                    }
                    
                    Unit selectedUnit = Managers.UnitsManager.AllyUnits[indexSelectedUnit];
                    selectedUnit.Select();
                    
                    Vector3 cameraPosition;
                    cameraPosition.x = selectedUnit.transform.position.x;
                    cameraPosition.y = FlyCamera.transform.position.y;
                    cameraPosition.z = selectedUnit.transform.position.z;
                    
                    FlyCamera.transform.position = cameraPosition;
                }
                else  if (CurrentFraction == Fraction.Enemy)
                {
                    Controller.DeselectAll();
                    if (indexSelectedUnit + 1 == Managers.UnitsManager.AllyUnits.Count)
                        indexSelectedUnit = 0;
                    
                    for (int i = indexSelectedUnit + 1; i < Managers.UnitsManager.EnemyUnits.Count; i++)
                    {
                        Unit unit = Managers.UnitsManager.EnemyUnits[i];

                        if (unit.Movement?.CountMovePoints > 0 ||
                            unit.Attackable?.CountAttackPoints > 0 ||
                            unit.HandCapable?.RechargeCount == 0)
                        {
                            indexSelectedUnit = i;
                            break;
                        }
                    }
                    
                    Unit selectedUnit = Managers.UnitsManager.AllyUnits[indexSelectedUnit];
                    selectedUnit.Select();
                    
                    Vector3 cameraPosition;
                    cameraPosition.x = selectedUnit.transform.position.x;
                    cameraPosition.y = FlyCamera.transform.position.y;
                    cameraPosition.z = selectedUnit.transform.position.z;
                    
                    FlyCamera.transform.position = cameraPosition;
                }
            }
            */
        }

        public void PassTurn()
        {
            CountTurn++;
            GameInfo.SetCountTurnText(Convert.ToString(CountTurn));
            
            MapManager.FogMode(true);
            
            if (CurrentFraction % 2 == 0)
            {
                foreach (var allyUnit in Managers.UnitsManager.AllyUnits)
                    allyUnit.ActivateAutoCapable(true);
                foreach (var enemyUnit in Managers.UnitsManager.EnemyUnits)
                    enemyUnit.ActivateAutoCapable(false);

                CurrentFraction++;
                if (CurrentFraction > 2)
                    CurrentFraction = 1;
                
                GameInfo.SetTurnFractionText("Enemy turn");

                foreach (var enemyUnit in Managers.UnitsManager.EnemyUnits)
                    enemyUnit.Viewer.DispelWarFog(enemyUnit.Hex);

                foreach (var enemyUnit in Managers.UnitsManager.EnemyUnits)
                {
                    enemyUnit.Movement?.ResetPoints(enemyUnit);
                    enemyUnit.Attackable?.ResetPoints(enemyUnit);
                    enemyUnit.HandCapable?.Recharge(enemyUnit);
                }
            }
            else
            {
                foreach (var allyUnit in Managers.UnitsManager.AllyUnits)
                    allyUnit.ActivateAutoCapable(false);
                foreach (var enemyUnit in Managers.UnitsManager.EnemyUnits)
                    enemyUnit.ActivateAutoCapable(true);

                CurrentFraction++;
                if (CurrentFraction > 2)
                    CurrentFraction = 1;
                
                GameInfo.SetTurnFractionText("Ally turn");
                
                foreach (var allyUnit in Managers.UnitsManager.AllyUnits)
                    allyUnit.Viewer.DispelWarFog(allyUnit.Hex);

                foreach (var allyUnit in Managers.UnitsManager.AllyUnits)
                {
                    allyUnit.Movement?.ResetPoints(allyUnit);
                    allyUnit.Attackable?.ResetPoints(allyUnit);
                    allyUnit.HandCapable?.Recharge(allyUnit);
                }
            }

            MapManager.ActivateAllCapableHexes();
        }
        
        private void OnDisable()
        {
            EndTurnButton.OnPressedEndTurnButton -= PassTurn;
        }
    }
}
