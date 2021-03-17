using UnityEngine;

namespace InputController
{
    public interface ISelectable
    {
        bool IsSelect { get; set; }
        
        void Select();
        
        void Deselect();
        
        void Action(GameObject selectObject);
    }
}