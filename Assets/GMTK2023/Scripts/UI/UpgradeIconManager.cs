using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class UpgradeIconManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private TextMeshProUGUI _costText;

    private Upgrade _upgrade;



    public void OnPointerDown(PointerEventData data)
    {
        EventManager.TriggerEvent("SelectUpgrade", _upgrade);
    }

    public void Initialize(UpgradeData data)
    {
        _upgrade = new(data, data.Effect, data.Amount, data.Cost);
        _titleText.text = data.Title;
        _costText.text = data.Cost.ToString();
        _image.sprite = data.Icon;
    }
}
