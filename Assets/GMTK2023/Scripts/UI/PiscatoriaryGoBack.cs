using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiscatoriaryGoBack : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        EventManager.TriggerEvent("PiscatoriaryGoBack");
    }
}
