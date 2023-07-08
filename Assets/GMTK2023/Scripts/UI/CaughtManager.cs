using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CaughtManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _entryNumber;
    private OccupantData _data;

    public void Initialize(OccupantData data)
    {
        _name.text = data.Name;
        _entryNumber.text = data.IndexID.ToString();
        _data = data;
    }

    public void OnPointerDown(PointerEventData data)
    {

    }
}
