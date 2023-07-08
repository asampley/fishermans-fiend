using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PiscatoriaryManager : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private Transform _listParent;
    [SerializeField]
    private Transform _listContentParent;
    [SerializeField]
    private Transform _entryParent;
    [SerializeField]
    private TextMeshProUGUI _entryNameText;

    [SerializeField]
    private Image _entryImage;
    [SerializeField]
    private TextMeshProUGUI _entryDescText;



    [SerializeField]
    private GameObject _uncaughtPrefab;
    [SerializeField]
    private GameObject _caughtPrefab;


    private Dictionary<OccupantData, bool> _hasBeenCaughtDict = new();



    private void OnEnable()
    {
        EventManager.AddListener("TogglePiscatoriary", _OnTogglePiscatoriary);
        EventManager.AddListener("CaughtOccupant", _OnCaughtOccupant);
        EventManager.AddListener("OpenPiscatoriaryEntry", _OnOpenEntry);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener("TogglePiscatoriary", _OnTogglePiscatoriary);
        EventManager.RemoveListener("CaughtOccupant", _OnCaughtOccupant);
        EventManager.RemoveListener("OpenPiscatoriaryEntry", _OnOpenEntry);

    }

    private void Awake()
    {
        foreach (OccupantData data in Globals.OCCUPANT_DATA)
        {
            _hasBeenCaughtDict.Add(data, true);
        }

        _RefreshOccupantData();
    }

    private void _OnCaughtOccupant(object data)
    {
        OccupantData occupantData = data as OccupantData;

        if (!_hasBeenCaughtDict[occupantData])
        {
            _hasBeenCaughtDict[occupantData] = true;
            _RefreshOccupantData();
        }
    }

    private void _RefreshOccupantData()
    {
        foreach (Transform child in _listContentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<OccupantData, bool> pair in _hasBeenCaughtDict)
        {
            if (pair.Value)
            {
                _GenerateEntryButton(pair.Key);
            }
            else
            {
                Instantiate(_uncaughtPrefab, _listContentParent);
            }
        }
    }

    private void _GenerateEntryButton(OccupantData data)
    {
        GameObject g = Instantiate(_caughtPrefab, _listContentParent);
        g.GetComponent<CaughtManager>().Initialize(data);
    }

    private void _OnOpenEntry(object data)
    {
        OccupantData occupantData = data as OccupantData;

        _listParent.gameObject.SetActive(false);
        _entryParent.gameObject.SetActive(true);

        _entryNameText.text = occupantData.Name;
        _entryImage.sprite = occupantData.Sprite;
        _entryDescText.text = occupantData.Desc;
    }

    private void _OnTogglePiscatoriary()
    {
        Debug.Log("running");
        _parent.gameObject.SetActive(!_parent.gameObject.activeInHierarchy);
    }
}
