using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscatoriaryManager : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private Transform _listParent;
    [SerializeField]
    private Transform _entryParent;
    [SerializeField]
    private GameObject _uncaughtPrefab;
    [SerializeField]
    private GameObject _caughtPrefab;


    private Dictionary<OccupantData, bool> _hasBeenCaughtDict = new();



    private void OnEnable()
    {
        EventManager.AddListener("CaughtOccupant", _OnCaughtOccupant);
    }
    private void OnDisable()
    {
        EventManager.RemoveListener("CaughtOccupant", _OnCaughtOccupant);
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
        OccupantData occupantData = data as OccupantData;

        if (!_hasBeenCaughtDict[occupantData])
        {
            _hasBeenCaughtDict[occupantData] = true;
            _RefreshOccupantData();
        }
    }

    private void _RefreshOccupantData()
    {
        foreach (Transform child in _parent)
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
                Instantiate(_uncaughtPrefab, _parent);
            }
        }
    }

    private void _GenerateEntryButton(OccupantData data)
    {
        GameObject g = Instantiate(_caughtPrefab, _parent);
        g.GetComponent<CaughtManager>().Initialize(data);
    }
}
