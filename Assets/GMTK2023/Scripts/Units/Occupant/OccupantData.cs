using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "OccupantData", menuName = "Scriptable Objects/OccupantData", order = 20)]
public class OccupantData : ScriptableObject
{
    public int IndexID;
    public string Name;
    public string Desc;
    public Sprite Sprite;
    public int Rarity;
    public int Biomass;
    public float Resistance;
    public float ProgressRate;
    public Vector2 GoTimeRange;
    public Vector2 StopTimeRange;

    public AudioSet fighting;
    public AudioSet fall;
    public AudioSet falling;
}
