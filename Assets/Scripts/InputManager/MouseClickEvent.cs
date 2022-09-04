using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseClickEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent mouseLeftClick;
    [SerializeField] private UnityEvent mouseRightClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            mouseLeftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            mouseRightClick.Invoke();
    }
}