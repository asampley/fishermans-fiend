using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CaughtManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _entryNumber;
    [SerializeField]
    private Image _sprite;
    private OccupantData _data;

    public void Initialize(OccupantData data)
    {
        _name.text = data.Name;
        _entryNumber.text = data.IndexID.ToString();
        _data = data;
        _sprite.sprite = data.Sprite;
    }

    public void OnPointerDown(PointerEventData data)
    {
        EventManager.TriggerEvent("OpenPiscatoriaryEntry", _data);
    }
}
