using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class UpgradeIconManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private TextMeshProUGUI _costText;
    private TextMeshProUGUI _desc;
    private string _descText;

    private Upgrade _upgrade;



    public void OnPointerDown(PointerEventData data)
    {
        EventManager.TriggerEvent("SelectUpgrade", _upgrade);
    }

    public void Initialize(UpgradeData data, TextMeshProUGUI desc)
    {
        _upgrade = new(data, data.Effect, data.Amount, data.Cost);
        _titleText.text = data.Title;
        _costText.text = data.Cost.ToString();
        _image.sprite = data.Icon;
        _desc = desc;
        _descText = data.Desc;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _desc.text = _descText;
    }
}
