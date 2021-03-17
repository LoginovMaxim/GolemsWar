using UnityEngine;
using UnityEngine.Events;

namespace InputController
{
    public interface IClickable
    {
        event UnityAction OnClickDown;
        event UnityAction OnClick;
        event UnityAction OnClickUp;
        
        ISelectable ClickDown(out GameObject selectObject);
        ISelectable Click(out GameObject selectObject);
        ISelectable ClickUp(out GameObject selectObject);
    }
}