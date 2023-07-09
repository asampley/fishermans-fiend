using UnityEngine;


public enum UpgradeEffect
{
    AddBiomassGainMultiplier,
    AddMaxTentacleLaunchVelocityStrength,
    AddMaxTentacles,
    AddAttackStrength,
    EnablePoisonDartAttack,
    EnableLaserBeamAttack,
    EnableInkCloud,
    EnableSirenSong,
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData", order = 7)]
public class UpgradeData : ScriptableObject
{
    public string Title;
    public Sprite Icon;
    public UpgradeEffect Effect;
    public float Amount;
    public int Cost;
}
