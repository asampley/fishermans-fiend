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


    private void Awake()
    {
        foreach (UpgradeData upgradeData in Globals.UPGRADE_DATA)
        {
            GameObject g = Instantiate(_upgradeIconPrefab, _upgradeParent);

            g.GetComponent<UpgradeIconManager>().Initialize(upgradeData);
        }
    }
}
