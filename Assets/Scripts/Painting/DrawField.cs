using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DrawField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UnityEvent MouseEnter;

    [SerializeField] private UnityEvent MouseExite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExite?.Invoke();
    }
}
