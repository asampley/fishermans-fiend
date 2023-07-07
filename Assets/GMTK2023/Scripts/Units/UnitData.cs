using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData", order = 10)]
public class UnitData : ScriptableObject
{
    public int UnitTypeId;
    public GameObject Prefab;
    public Sprite Sprite;
    public float Speed;
}
