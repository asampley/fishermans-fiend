using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameStatsManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _biomassText;
    [SerializeField]
    private TextMeshProUGUI _awarenessText;

    private void OnEnable()
    {
        EventManager.AddListener("UpdateBiomass", _OnUpdateBiomass);
        EventManager.AddListener("UpdateAwareness", _OnUpdateAwareness);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("UpdateBiomass", _OnUpdateBiomass);
        EventManager.RemoveListener("UpdateAwareness", _OnUpdateAwareness);
    }


    private void _OnUpdateBiomass(object data)
    {
        Debug.Log("runnign");
        int amount = (int)data;
        _biomassText.text = amount.ToString();
    }

    private void _OnUpdateAwareness(object data)
    {
        Debug.Log("runnign");
        int amount = (int)data;
        _awarenessText.text = amount.ToString();
    }
}
