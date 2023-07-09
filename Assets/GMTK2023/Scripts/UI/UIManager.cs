using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _upgradeMenuParent;
    [SerializeField]
    private GameObject _gameOverParent;
    [SerializeField]
    private GameObject _tutorialParent;

    [SerializeField]
    private TextMeshProUGUI _biomassAmountText;
    [SerializeField]
    private GameObject _victoryObject;
    [SerializeField]
    private GameObject _loseObject;
    [SerializeField]
    private PiscatoriaryManager _piscatoriaryManager;
    [SerializeField]
    private TextMeshProUGUI _completionText;

    [SerializeField]
    private Image _tutorialButtonSprite;
    [SerializeField]
    private Sprite _xButtonIcon;
    [SerializeField]
    private Sprite _tutorialIcon;

    private void OnEnable()
    {
        EventManager.AddListener("UpdateBiomass", _OnUpdateBiomass);
        EventManager.AddListener("ToggleTutorial", _OnToggleTutorial);
        GameManager.Instance.FinishDay += _ShowUpgradeScreen;
        GameManager.Instance.LoseGame += _ShowLossScreen;
        GameManager.Instance.WinGame += _ShowVictoryScreen;
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("UpdateBiomass", _OnUpdateBiomass);
        EventManager.RemoveListener("ToggleTutorial", _OnToggleTutorial);
        GameManager.Instance.FinishDay -= _ShowUpgradeScreen;
        GameManager.Instance.WinGame -= _ShowVictoryScreen;

    }

    private void _ShowUpgradeScreen()
    {
        _upgradeMenuParent.SetActive(true);
    }

    private void _ShowVictoryScreen()
    {
        _victoryObject.SetActive(true);
        _loseObject.SetActive(false);
        _completionText.text = $"{_piscatoriaryManager.GetTotalCaught()} / {_piscatoriaryManager.GetTotal()}";
        _gameOverParent.gameObject.SetActive(true);
    }

    private void _ShowLossScreen()
    {
        _victoryObject.SetActive(false);
        _loseObject.SetActive(true);
        _completionText.text = $"{_piscatoriaryManager.GetTotalCaught()} / {_piscatoriaryManager.GetTotal()}";
        _gameOverParent.gameObject.SetActive(true);
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

    private void _OnToggleTutorial()
    {
        _tutorialParent.SetActive(!_tutorialParent.activeInHierarchy);

        if (_tutorialParent.activeInHierarchy)
        {
            GameManager.Instance.SetPauseGame(true);
            _tutorialButtonSprite.sprite = _xButtonIcon;
        }
        else
        {
            GameManager.Instance.SetPauseGame(false);
            _tutorialButtonSprite.sprite = _tutorialIcon;
        }
    }
}
