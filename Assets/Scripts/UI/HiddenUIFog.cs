using System;
using InputController;
using Units;
using UnityEngine;

namespace UI
{
    public class HiddenUIFog : MonoBehaviour
    {
        public Controller Controller;
        public CanvasGroup CanvasGroup;
        public Unit Unit;

        private void OnEnable()
        {
            if (Controller == null)
                Controller = FindObjectOfType<Controller>();

            Controller.OnSomeUpdated += TryShowUI;
        }

        private void Start()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void TryShowUI()
        {
            CanvasGroup.alpha = 1 - Convert.ToInt32(Unit.Hex.Fog.activeSelf);
        }

        private void OnDisable()
        {
            Controller.OnSomeUpdated -= TryShowUI;
        }
    }
}
