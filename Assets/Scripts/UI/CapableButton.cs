using System;
using System.Collections.Generic;
using InputController;
using Units;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class CapableButton : MonoBehaviour
    {
        public event UnityAction OnPressedCapableButton;
        
        public Controller Controller;
        public CanvasGroup CanvasGroup;

        private Button _button;
        
        private void OnEnable()
        {
            Controller.OnSomeSelected += CheckCapableButton;

            _button = GetComponent<Button>();
            _button.onClick.AddListener(UnitCapableConnection);
        }

        private void Start()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
            CanvasGroup.alpha = 0f;
            CanvasGroup.interactable = false;
        }

        private void UnitCapableConnection()
        {
            OnPressedCapableButton?.Invoke();
            CanvasGroup.alpha = 0.5f;
            CanvasGroup.interactable = false;
        }
        
        private void CheckCapableButton(GameObject gameObject)
        {
            if (gameObject == null)
            {
                CanvasGroup.alpha = 0f;
                CanvasGroup.interactable = false;
                return;
            }
            
            if (gameObject.TryGetComponent(out Unit unit))
            {
                if (unit.HandCapable == null)
                {
                    CanvasGroup.alpha = 0f;
                    CanvasGroup.interactable = false;
                    return;
                }
                
                CanvasGroup.alpha = 0.5f;
                CanvasGroup.interactable = false;

                if (unit.HandCapable.RechargeCount == 0)
                {
                    CanvasGroup.alpha = 1f;
                    CanvasGroup.interactable = true;
                }
            }
            else
            {
                CanvasGroup.alpha = 0f;
                CanvasGroup.interactable = false;
            }
        }

        private void OnDisable()
        {
            Controller.OnSomeSelected -= CheckCapableButton;
            _button.onClick.RemoveListener(UnitCapableConnection);
        }
    }
}