using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeScreenManager : MonoBehaviour
{
    [SerializeField]
    private Transform _upgradeParent;
    [SerializeField]
    private GameObject _upgradeIconPrefab;

    private Dictionary<UpgradeData, bool> _hasPurchasedList = new();


    private void OnEnable()
    {
        EventManager.AddListener("SuccessfullyBuyUpgrade", _OnSuccessBuy);
        _PopulateList();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("SuccessfullyBuyUpgrade", _OnSuccessBuy);
    }

    private void Awake()
    {
        foreach (UpgradeData upgradeData in Globals.UPGRADE_DATA)
        {
            _hasPurchasedList.Add(upgradeData, false);
        }
    }


    private void _PopulateList()
    {
        Debug.Log("Running");
        foreach (Transform child in _upgradeParent)
        {
            Destroy(child.gameObject);
        }
        foreach (KeyValuePair<UpgradeData, bool> pair in _hasPurchasedList)
        {
            if (!pair.Value)
            {
                GameObject g = Instantiate(_upgradeIconPrefab, _upgradeParent);

                g.GetComponent<UpgradeIconManager>().Initialize(pair.Key);                
            }
        }        
    }

    private void _OnSuccessBuy(object data)
    {
        Upgrade upgrade = data as Upgrade;
        Debug.Log(upgrade.Effect);
        _hasPurchasedList[upgrade.Data] = true;

        _PopulateList();

    }

}
