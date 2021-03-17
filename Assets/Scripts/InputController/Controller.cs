using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UI;
using Units;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace InputController
{
    public class Controller : MonoBehaviour
    {
        public event UnityAction<GameObject> OnSomeSelected;
        public event UnityAction OnSomeUpdated;
        
        public IClickable Clickable;

        public ISelectable Selectable1;
        public ISelectable Selectable2;

        public GameObject SelectObject1;
        public GameObject SelectObject2;

        public GameManager GameManager;
        
        private void Start()
        {
            Clickable = gameObject.AddComponent<MouseController>();
            GameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                
                if (Selectable1 == null)
                {
                    Selectable1 = Clickable.ClickDown(out SelectObject1);

                    if (CheckSelectFraction(SelectObject1) == false)
                        return;
                    
                    Select(Selectable1);
                    OnSomeSelected?.Invoke(SelectObject1);
                }
                else
                {
                    Selectable2 = Clickable.ClickDown(out SelectObject2);
                    
                    if (CheckSelectFraction(SelectObject2) == false)
                        return;
                    
                    if (SelectObject1 == SelectObject2)
                    {
                        Deselect(ref Selectable2, ref SelectObject2);
                        return;
                    }
                    
                    if (SelectObject2 != null)
                        Selectable1.Action(SelectObject2);
                    
                    ISelectable selectable = Selectable2;
                    GameObject gameObject = SelectObject2;

                    DeselectAll();
                    
                    Selectable1 = selectable;
                    SelectObject1 = gameObject;
                    
                    Select(Selectable1);
                    OnSomeSelected?.Invoke(SelectObject1);
                    
                    OnSomeUpdated?.Invoke();
                    
                    /*
                    if (SelectObject2 != null)
                        Selectable1.Action(SelectObject2);
                    
                    Deselect(ref Selectable1, ref SelectObject1);
                    Deselect(ref Selectable2, ref SelectObject2);
                    
                    Select(Selectable1);
                    OnSomeSelected?.Invoke(SelectObject1);
                    
                    OnSomeUpdated?.Invoke();
                    */
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                DeselectAll();
            }
        }

        public void DeselectAll()
        {
            Deselect(ref Selectable1, ref SelectObject1);
            Deselect(ref Selectable2, ref SelectObject2);
        }
        
        private bool CheckSelectFraction(GameObject selectObject)
        {
            if (selectObject != null)
            {
                if (selectObject.TryGetComponent(out Unit unit))
                {
                    if (unit.Fraction != GameManager.CurrentFraction)
                        return false;
                }
            }
            
            return true;
        }

        private void Select(ISelectable selectable)
        {
            if (selectable != null)
                selectable.Select();
        }

        private void Deselect(ref ISelectable selectable, ref GameObject selectObject)
        {
            if (selectable != null)
            {
                selectable.Deselect();
                selectable = null;
                selectObject = null;
            }
        }
    }
}