using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class EndTurnButton : MonoBehaviour
    {
        public event UnityAction OnPressedEndTurnButton; 
    
        private Button _button;
        
        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(CallEndTurnButton);
        }

        public void CallEndTurnButton()
        {
            OnPressedEndTurnButton?.Invoke();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(CallEndTurnButton);
        }
    }
}
