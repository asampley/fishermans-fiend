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
        EventManager.AddListener("PiscatoriaryGoBack", _OnGoBack);
        EventManager.AddListener("CaughtOccupant", _OnCaughtOccupant);
        EventManager.AddListener("OpenPiscatoriaryEntry", _OnOpenEntry);
        
    }
    private void OnDisable()
    {
        EventManager.RemoveListener("TogglePiscatoriary", _OnTogglePiscatoriary);
        EventManager.RemoveListener("PiscatoriaryGoBack", _OnGoBack);
        EventManager.RemoveListener("CaughtOccupant", _OnCaughtOccupant);
        EventManager.RemoveListener("OpenPiscatoriaryEntry", _OnOpenEntry);

    }

    private void Awake()
    {
        foreach (OccupantData data in Globals.OCCUPANT_DATA)
        {
            _hasBeenCaughtDict.Add(data, false);
        }

        _RefreshOccupantData();
    }

    private void _OnCaughtOccupant(object data)
    {
        Debug.Log("pee");
        OccupantManager occupantManager= data as OccupantManager;
        OccupantData occupantData = occupantManager.data;

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
        _parent.gameObject.SetActive(!_parent.gameObject.activeInHierarchy);


        if (_parent.gameObject.activeInHierarchy)
        {
            GameManager.Instance.SetPauseGame(true);
        }
        else
        {
            GameManager.Instance.SetPauseGame(false);
        }
    }

    private void _OnGoBack()
    {
        _entryParent.gameObject.SetActive(false);
        _listParent.gameObject.SetActive(true);
    }
}
