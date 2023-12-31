using UnityEngine;

public static class Globals
{
    public static float X_EDGE = 10f;
    public static float SURFACE_HEIGHT = 8f;
    public static float Y_MIN = 1.5f;
    public static float Y_MAX = 7f;
    public static float SUN_OBJECT_Y = 9f;

    public static float DAY_DURATION = 50f;
    public static float NIGHT_DURATION = 40f;
    public static float DAY_TOTAL_DURATION = DAY_DURATION + NIGHT_DURATION;

    public static float MIN_TIME_BETWEEN_VICTIM_SPAWNS = 3f;
    public static float MAX_TIME_BETWEEN_VICTIM_SPAWNS = 7f;

    public static float MIN_TIME_BETWEEN_OBSTACLE_SPAWNS = 3f;
    public static float MAX_TIME_BETWEEN_OBSTACLE_SPAWNS = 6f;

    public static float MIN_TIME_BETWEEN_ENEMY_SPAWNS = 10f;
    public static float MAX_TIME_BETWEEN_ENEMY_SPAWNS = 20f;

    public static KeyCode[] KEYS =
    {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
    };

    public static DayData[] DAY_DATA;
    public static UpgradeData[] UPGRADE_DATA;
    public static OccupantData[] OCCUPANT_DATA;


    public static string DAY_DATA_FOLDER = "ScriptableObjects/DayData";
    public static string UPGRADE_DATA_FOLDER = "ScriptableObjects/UpgradeData";
    public static string OCCUPANT_DATA_FOLDER = "ScriptableObjects/OccupantData";
}
