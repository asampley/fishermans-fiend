using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private void Awake()
    {
        LoadGameData();
    }

    public void LoadGameData()
    {
        Globals.DAY_DATA = Resources.LoadAll<DayData>(Globals.DAY_DATA_FOLDER);
        Globals.UPGRADE_DATA = Resources.LoadAll<UpgradeData>(Globals.UPGRADE_DATA_FOLDER);
        Globals.OCCUPANT_DATA = Resources.LoadAll<OccupantData>(Globals.OCCUPANT_DATA_FOLDER);
    }
}
