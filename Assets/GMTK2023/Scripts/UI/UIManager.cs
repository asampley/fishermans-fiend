using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _upgradeMenuParent;
    [SerializeField]
    private GameObject _gameOverParent;
    [SerializeField]
    private TextMeshProUGUI _biomassAmountText;


    private void OnEnable()
    {
        EventManager.AddListener("UpdateBiomass", _OnUpdateBiomass);
        GameManager.Instance.FinishDay += _ShowUpgradeScreen;
        GameManager.Instance.LoseGame += _ShowLossScreen;

    }

    private void OnDisable()
    {
        EventManager.RemoveListener("UpdateBiomass", _OnUpdateBiomass);
        GameManager.Instance.FinishDay -= _ShowUpgradeScreen;
        GameManager.Instance.LoseGame -= _ShowLossScreen;

    }

    private void _ShowUpgradeScreen()
    {
        _upgradeMenuParent.SetActive(true);
    }

    private void _ShowLossScreen()
    {
        _gameOverParent.SetActive(true);
    }

    public void GoToNextDay()
    {
        _upgradeMenuParent.SetActive(false);
        GameManager.Instance.StartNextDay();
    }

    private void _OnUpdateBiomass(object data)
    {
        int amount = (int)data;
        _biomassAmountText.text = amount.ToString();        
    }
}
