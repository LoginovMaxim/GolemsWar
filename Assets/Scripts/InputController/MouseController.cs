using System;
using UnityEngine;
using UnityEngine.Events;

namespace InputController
{
    public class MouseController : MonoBehaviour, IClickable
    {
        public ISelectable Selectable;

        public event UnityAction OnClickDown;
        public event UnityAction OnClick;
        public event UnityAction OnClickUp;

        public Camera Camera;
        
        private void Start()
        {
            Camera = Camera.main;
        }

        public ISelectable ClickDown(out GameObject selectObject)
        {
            OnClickDown?.Invoke();
            return GetHitGameObject(out selectObject);
        }

        public ISelectable Click(out GameObject selectObject)
        {
            OnClick?.Invoke();
            return GetHitGameObject(out selectObject);
        }

        public ISelectable ClickUp(out GameObject selectObject)
        {
            OnClickUp?.Invoke();
            return GetHitGameObject(out selectObject);
        }

        private ISelectable GetHitGameObject(out GameObject selectObject)
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                selectObject = hit.collider.gameObject;
                if (hit.collider.gameObject.TryGetComponent(out ISelectable selectable))
                {
                    OnClickDown?.Invoke();
                    return selectable;
                }
            }

            selectObject = null;
            return null;
        }
    }
}