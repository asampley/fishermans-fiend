using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeMenuParent;
    [SerializeField] private GameObject _gameOverParent;



    private void OnEnable()
    {
        GameManager.Instance.FinishDay += _ShowUpgradeScreen;
        GameManager.Instance.LoseGame += _ShowLossScreen;

    }

    private void OnDisable()
    {
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
}
