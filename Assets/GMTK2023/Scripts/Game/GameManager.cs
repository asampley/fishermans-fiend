using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private bool _gameIsPaused;
    public bool GameIsPaused => _gameIsPaused;

    private int _currentDay = 0;
    public int CurrentDay => _currentDay;
    private DayData _currentDayData;
    private float _dayTimer;
    private bool _isNight;

    private float _spawnEnemyTimer;
    private float _spawnObstacleTimer;
    private float _spawnVictimTimer;
    private float _nextEnemySpawn;
    private float _nextObstacleSpawn;
    private float _nextVictimSpawn;

    private int _currentBiomass;
    public int CurrentBiomass => _currentBiomass;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _SelectNextDay();
    }

    private void Update()
    {
        if (_gameIsPaused) return;

        if (_currentDayData.EnemiesToSpawn.Length > 0)
        {
            _CheckEnemySpawn();
        }
        if (_currentDayData.ObstaclesToSpawn.Length > 0)
        {
            _CheckObstacleSpawn();
        }
        if (_currentDayData.VictimsToSpawn.Length > 0)
        {
            _CheckVictimSpawn();
        }

        _dayTimer += Time.deltaTime;
        if (!_isNight && _dayTimer >= Globals.DAY_DURATION) _StartNight();
        if (_dayTimer >= Globals.DAY_TOTAL_DURATION) _EndDay();
    }

    private void _CheckEnemySpawn()
    {
        _spawnEnemyTimer += Time.deltaTime;
        if (_spawnEnemyTimer >= _nextEnemySpawn)
        {
            EnemyData enemy = _currentDayData.EnemiesToSpawn[Random.Range(0, _currentDayData.EnemiesToSpawn.Length)];
            _SpawnEnemy(enemy);
            _spawnEnemyTimer = 0f;
            _nextEnemySpawn = Random.Range(Globals.MIN_TIME_BETWEEN_ENEMY_SPAWNS + _currentDayData.SpawnCooldown, Globals.MAX_TIME_BETWEEN_ENEMY_SPAWNS + _currentDayData.SpawnCooldown);
        }
    }

    private void _CheckObstacleSpawn()
    {
        _spawnObstacleTimer += Time.deltaTime;
        if (_spawnObstacleTimer >= _nextObstacleSpawn)
        {
            ObstacleData obstacle = _currentDayData.ObstaclesToSpawn[Random.Range(0, _currentDayData.ObstaclesToSpawn.Length)];
            _SpawnObstacle(obstacle);
            _spawnObstacleTimer = 0f;
            _nextObstacleSpawn = Random.Range(Globals.MIN_TIME_BETWEEN_OBSTACLE_SPAWNS + _currentDayData.SpawnCooldown, Globals.MAX_TIME_BETWEEN_OBSTACLE_SPAWNS + _currentDayData.SpawnCooldown);
        }
    }

    private void _CheckVictimSpawn()
    {
        _spawnVictimTimer += Time.deltaTime;
        if (_spawnVictimTimer >= _nextVictimSpawn)
        {
            Debug.Log("Running");
            VictimData Victim = _currentDayData.VictimsToSpawn[Random.Range(0, _currentDayData.VictimsToSpawn.Length)];
            _SpawnVictim(Victim);
            _spawnVictimTimer = 0f;
            _nextVictimSpawn = Random.Range(Globals.MIN_TIME_BETWEEN_VICTIM_SPAWNS + _currentDayData.SpawnCooldown, Globals.MAX_TIME_BETWEEN_VICTIM_SPAWNS + _currentDayData.SpawnCooldown);
        }
    }

    private void _SelectNextDay()
    {
        _spawnEnemyTimer = 0f;
        _spawnObstacleTimer = 0f;
        _spawnVictimTimer = 0f;
        _nextEnemySpawn = Random.Range(Globals.MIN_TIME_BETWEEN_ENEMY_SPAWNS, Globals.MAX_TIME_BETWEEN_ENEMY_SPAWNS);
        _nextObstacleSpawn = Random.Range(Globals.MIN_TIME_BETWEEN_OBSTACLE_SPAWNS, Globals.MAX_TIME_BETWEEN_OBSTACLE_SPAWNS);
        _nextVictimSpawn = Random.Range(Globals.MIN_TIME_BETWEEN_VICTIM_SPAWNS, Globals.MAX_TIME_BETWEEN_VICTIM_SPAWNS);

        _currentDay++;
        _currentDayData = Globals.DAY_DATA.Where((DayData x) => x.DayNumber == _currentDay).First();
        _dayTimer = 0f;
    }

    private void _StartNight()
    {
        _isNight = true;
        Debug.Log("It is night time");
    }

    private void _EndDay()
    {
        _gameIsPaused = true;
        Time.timeScale = 0;
        Debug.Log("Day is over");

    }

    private void _SpawnEnemy(EnemyData data)
    {
        bool spawnOnLeft = Random.Range(0, 2) == 0;

        if (data.SpawnsBelowSurface)
        {
            float height = Random.Range(Globals.Y_MIN, Globals.Y_MAX);
            new Enemy(data, spawnOnLeft, height);
        }
        else
        {
            new Enemy(data, spawnOnLeft, Globals.SURFACE_HEIGHT);
        }
    }

    private void _SpawnObstacle(ObstacleData data)
    {
        bool spawnOnLeft = Random.Range(0, 2) == 0;

        if (data.SpawnsBelowSurface)
        {
            float height = Random.Range(Globals.Y_MIN, Globals.Y_MAX);
            new Obstacle(data, spawnOnLeft, height);
        }
        else
        {
            new Obstacle(data, spawnOnLeft, Globals.SURFACE_HEIGHT);
        }
    }
    private void _SpawnVictim(VictimData data)
    {
        bool spawnOnLeft = Random.Range(0, 2) == 0;

        if (data.SpawnsBelowSurface)
        {
            float height = Random.Range(Globals.Y_MIN, Globals.Y_MAX);
            new Victim(data, spawnOnLeft, height);
        }
        else
        {
            new Victim(data, spawnOnLeft, Globals.SURFACE_HEIGHT);
        }        
    }
}
