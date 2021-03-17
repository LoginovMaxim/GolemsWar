using System;
using System.Collections;
using System.Collections.Generic;
using Environment;
using Environment.Hex;
using Gameplay;
using InputController;
using UI;
using Units.Possibilities.Attack;
using Units.Possibilities.Capable.Auto;
using Units.Possibilities.Capable.Hand;
using Units.Possibilities.Capable.Passive;
using Units.Possibilities.Move;
using Units.Possibilities.TakeDamage;
using Units.Possibilities.View;
using Units.UnitStats;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Units
{
    public class Unit : MonoBehaviour, ISelectable
    {
        public event UnityAction<Unit> OnDied;
        
        [Header("Visual")] 
        public List<Animator> Animators;
        
        [HideInInspector] public CapableButton CapableButton;
        [HideInInspector] public Controller Controller;
        [HideInInspector] public Hex Hex;
        
        [Header("Stats")]
        public int Fraction;
        public Element element;
        public int Potential;
        public Damage Damage;
        public Health Health;
        public Armor Armor;
        public float DistanceAboveGround;
        
        [Header("Possibilities")]
        public IMovement Movement;
        public IAttackable Attackable;
        public IAutoCapable AutoCapable;
        public IHandCapable HandCapable;
        public IPassiveCapable PassiveCapable;
        public IDamageable Damageable;
        public Viewer Viewer;
        
        [HideInInspector]
        public MapGenerator MapGenerator;
        [HideInInspector]
        public MapManager MapManager;

        private bool _isSelect;

        private List<IPassiveCapable> _passiveCapables;

        public bool IsSelect
        {
            get => _isSelect;
            set => _isSelect = value;
        }
        
        private void Awake()
        {
            Damage.Init();
            Health.Init();
            Armor.Init();
        }

        private void OnEnable()
        {
            if (Controller == null)
                Controller = FindObjectOfType<Controller>();

            if (CapableButton == null)
                CapableButton = FindObjectOfType<CapableButton>();
            
            Health.OnNuliffiedValue += Die;
            Controller.OnSomeUpdated += SomeUpdate;
            CapableButton.OnPressedCapableButton += ActivateHandCapable;
        }

        private void Start()
        {
            MapGenerator = FindObjectOfType<MapGenerator>();
            MapManager = FindObjectOfType<MapManager>();
            //Invoke("PlaceUnit", 1f);

            if (TryGetComponent(out IMovement movement))
                Movement = movement;
            else
                Movement = null;

            if (TryGetComponent(out IAttackable attackable))
                Attackable = attackable;
            else
                Attackable = null;

            if (TryGetComponent(out IAutoCapable autoCapable))
                AutoCapable = autoCapable;
            else
                AutoCapable = null;

            if (TryGetComponent(out IHandCapable handCapable))
                HandCapable = handCapable;
            else
                HandCapable = null;
            
            if (TryGetComponent(out IPassiveCapable passiveCapable))
                PassiveCapable = passiveCapable;
            else
                PassiveCapable = null;
            
            if (TryGetComponent(out IDamageable damageable))
                Damageable = damageable;
            else
                Damageable = null;
        }
        
        private void SomeUpdate()
        {
            PassiveCapable?.PassiveAction(this);
        }

        private void ActivateHandCapable()
        {
            if (IsSelect == false)
                return;
            
            if (HandCapable == null)
                return;
            
            HandCapable.ButtonResponse(this);
            HandCapable.PendingAction = true;
        }

        public void ActivateAutoCapable(bool finalTurn)
        {
            
        }
        
        // temporary
        private void PlaceUnit()
        {
            Hex hex;
            while (true)
            {
                hex = MapGenerator.Hexes[Random.Range(0, MapGenerator.Hexes.Count)];
                
                if (hex.Playable && !hex.HaveAnyUnit())
                    break;
            }
            
            Hex = hex;
            Hex.Units.Add(this);
            transform.position = Hex.transform.position + DistanceAboveGround * Vector3.up;
        }

        public void Select()
        {
            IsSelect = true;
            
            MapManager.ResetCheckAllHexes();
            for (int i = 0; i < 5; i++)
                MapManager.ResetHexPoints(i);
            
            if (Movement != null)
            {
                if (Movement.CountMovePoints > 0)
                {
                    Movement.FindPath(this);
                }
            }

            if (Attackable != null)
            {
                if (Attackable.CountAttackPoints > 0)
                {
                    Attackable.FindTarget(this);
                }
            }
        }

        public void Deselect()
        {
            IsSelect = false;
            
            MapManager.HideAllHighlighters();
        }

        public void Action(GameObject selectObject)
        {
            if (selectObject.TryGetComponent(out Hex hex))
            {
                if (Movement != null)
                {
                    if (HandCapable?.PendingAction == false || HandCapable == null)
                    {
                        if (hex.CanReceiveUnit(this))
                        {
                            Movement.Move(this, hex);
                            Viewer.DispelWarFog(Hex);
                        }
                    }
                }

                if (Attackable != null)
                {
                    if (HandCapable?.PendingAction == false || HandCapable == null)
                    {
                        if (hex.HaveEnemyUnit(this))
                        {
                            if (Attackable.CountAttackPoints > 0)
                            {
                                Attackable.Attack(this, hex);
                            }
                        }
                    }
                }
                
                if (HandCapable != null)
                {
                    if (HandCapable.PendingAction)
                    {
                        for (int i = 2; i < 6; i++)
                        {
                            if (hex.Points[i] > 0)
                            {
                                if (HandCapable?.RechargeCount == 0)
                                    HandCapable.HandAction(this, hex);
                            }
                        }
                        
                        HandCapable.PendingAction = false;
                    }
                }
            }
        }

        public void Die()
        {
            Movement = null;
            Attackable = null;
            
            foreach (var animator in Animators)
            {
                animator.SetTrigger("Die");
            }

            StartCoroutine(Dying(2f));
        }

        private void OnDisable()
        {
            Health.OnNuliffiedValue -= Die;
            Controller.OnSomeUpdated -= SomeUpdate;
            CapableButton.OnPressedCapableButton -= ActivateHandCapable;
        }

        private IEnumerator Dying(float duration)
        {
            Vector3 livePosition = transform.position;
            Vector3 diePosition = livePosition + Vector3.down * 0.5f;
         
            Hex.Units.Remove(this);
            OnDied?.Invoke(this);
            yield return new WaitForSeconds(duration);
            
            float t = 0;
            while (t < duration)
            {
                transform.position = Vector3.Lerp(livePosition, diePosition, t / duration);
                t += Time.deltaTime;
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}
