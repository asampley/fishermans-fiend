using UnityEngine;

public static class Globals
{
    public static float X_EDGE = 10f;
    public static float SURFACE_HEIGHT = 7f;
    public static float Y_MIN = 0.5f;
    public static float Y_MAX = 6f;

    public static float DAY_DURATION = 30f;
    public static float NIGHT_DURATION = 15f;
    public static float DAY_TOTAL_DURATION = DAY_DURATION + NIGHT_DURATION;

    public static float MIN_TIME_BETWEEN_VICTIM_SPAWNS = 3f;
    public static float MAX_TIME_BETWEEN_VICTIM_SPAWNS = 7f;

    public static float MIN_TIME_BETWEEN_OBSTACLE_SPAWNS = 5f;
    public static float MAX_TIME_BETWEEN_OBSTACLE_SPAWNS = 15f;

    public static float MIN_TIME_BETWEEN_ENEMY_SPAWNS = 10f;
    public static float MAX_TIME_BETWEEN_ENEMY_SPAWNS = 20f;

    public static KeyCode[] KEYS =
    {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
    };

    public static DayData[] DAY_DATA;
    public static UpgradeData[] UPGRADE_DATA;


    public static string DAY_DATA_FOLDER = "ScriptableObjects/DayData";
    public static string UPGRADE_DATA_FOLDER = "ScriptableObjects/UpgradeData";
}
