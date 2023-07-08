using System;
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
    private int _currentAwareness;
    public int CurrentAwareness => _currentAwareness;

    //Upgradables
    private float _attackStrength = 1f;
    public float AttackStrength => _attackStrength;
    private float _biomassMultiplier = 1f;
    public float BiomassMultipier => _biomassMultiplier;
    private float _maxTentacleLaunchStrength = 8f;
    public float MaxTentacleLaunchStrength => _maxTentacleLaunchStrength;
    private int _maxTentacles = 1;
    public int MaxTentacles => _maxTentacles;
    private float _tentactleStrength = 1f;
    public float TentacleStrength => _tentactleStrength;

    private float _tentacleVelocityScale = 1f;
    public float TentacleVelocityScale => _tentacleVelocityScale;
    private bool _canPoisonDart = false;
    public bool CanPoisonDart => _canPoisonDart;
    private bool _canLaserBeam = false;
    public bool CanLaserBeam => _canLaserBeam;
    private bool _canInkPouch = false;
    public bool CanInkPouch => _canInkPouch;


    public event Action FinishDay;
    public event Action LoseGame;


    private void OnEnable()
    {
        EventManager.AddListener("SelectUpgrade", _OnSelectUpgrade);
    }

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
            EnemyData enemy = _currentDayData.EnemiesToSpawn[UnityEngine.Random.Range(0, _currentDayData.EnemiesToSpawn.Length)];
            _SpawnEnemy(enemy);
            _spawnEnemyTimer = 0f;
            _nextEnemySpawn = UnityEngine.Random.Range(Globals.MIN_TIME_BETWEEN_ENEMY_SPAWNS + _currentDayData.SpawnCooldown, Globals.MAX_TIME_BETWEEN_ENEMY_SPAWNS + _currentDayData.SpawnCooldown);
        }
    }

    private void _CheckObstacleSpawn()
    {
        _spawnObstacleTimer += Time.deltaTime;
        if (_spawnObstacleTimer >= _nextObstacleSpawn)
        {
            ObstacleData obstacle = _currentDayData.ObstaclesToSpawn[UnityEngine.Random.Range(0, _currentDayData.ObstaclesToSpawn.Length)];
            _SpawnObstacle(obstacle);
            _spawnObstacleTimer = 0f;
            _nextObstacleSpawn = UnityEngine.Random.Range(Globals.MIN_TIME_BETWEEN_OBSTACLE_SPAWNS + _currentDayData.SpawnCooldown, Globals.MAX_TIME_BETWEEN_OBSTACLE_SPAWNS + _currentDayData.SpawnCooldown);
        }
    }

    private void _CheckVictimSpawn()
    {
        _spawnVictimTimer += Time.deltaTime;
        if (_spawnVictimTimer >= _nextVictimSpawn)
        {
            Debug.Log("Running");
            VictimData Victim = _currentDayData.VictimsToSpawn[UnityEngine.Random.Range(0, _currentDayData.VictimsToSpawn.Length)];
            _SpawnVictim(Victim);
            _spawnVictimTimer = 0f;
            _nextVictimSpawn = UnityEngine.Random.Range(Globals.MIN_TIME_BETWEEN_VICTIM_SPAWNS + _currentDayData.SpawnCooldown, Globals.MAX_TIME_BETWEEN_VICTIM_SPAWNS + _currentDayData.SpawnCooldown);
        }
    }

    private void _SelectNextDay()
    {
        _spawnEnemyTimer = 0f;
        _spawnObstacleTimer = 0f;
        _spawnVictimTimer = 0f;
        _nextEnemySpawn = UnityEngine.Random.Range(Globals.MIN_TIME_BETWEEN_ENEMY_SPAWNS, Globals.MAX_TIME_BETWEEN_ENEMY_SPAWNS);
        _nextObstacleSpawn = UnityEngine.Random.Range(Globals.MIN_TIME_BETWEEN_OBSTACLE_SPAWNS, Globals.MAX_TIME_BETWEEN_OBSTACLE_SPAWNS);
        _nextVictimSpawn = UnityEngine.Random.Range(Globals.MIN_TIME_BETWEEN_VICTIM_SPAWNS, Globals.MAX_TIME_BETWEEN_VICTIM_SPAWNS);

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

        if (_currentBiomass >= _currentDayData.RequiredBiomass)
        {
            FinishDay?.Invoke();
        }
        else
        {
            LoseGame?.Invoke();
        }
    }

    private void _SpawnEnemy(EnemyData data)
    {
        bool spawnOnLeft = UnityEngine.Random.Range(0, 2) == 0;

        if (data.SpawnsBelowSurface)
        {
            float height = UnityEngine.Random.Range(Globals.Y_MIN, Globals.Y_MAX);
            new Enemy(data, spawnOnLeft, height);
        }
        else
        {
            new Enemy(data, spawnOnLeft, Globals.SURFACE_HEIGHT);
        }
    }

    private void _SpawnObstacle(ObstacleData data)
    {
        bool spawnOnLeft = UnityEngine.Random.Range(0, 2) == 0;

        if (data.SpawnsBelowSurface)
        {
            float height = UnityEngine.Random.Range(Globals.Y_MIN, Globals.Y_MAX);
            new Obstacle(data, spawnOnLeft, height);
        }
        else
        {
            new Obstacle(data, spawnOnLeft, Globals.SURFACE_HEIGHT);
        }
    }
    private void _SpawnVictim(VictimData data)
    {
        bool spawnOnLeft = UnityEngine.Random.Range(0, 2) == 0;

        if (data.SpawnsBelowSurface)
        {
            float height = UnityEngine.Random.Range(Globals.Y_MIN, Globals.Y_MAX);
            new Victim(data, spawnOnLeft, height);
        }
        else
        {
            new Victim(data, spawnOnLeft, Globals.SURFACE_HEIGHT);
        }
    }

    private void _OnSelectUpgrade(object data)
    {
        Upgrade upgrade = data as Upgrade;
        _currentBiomass = 50;

        if (_currentBiomass < upgrade.Cost)
        {
            Debug.Log("Too Poor");
        }
        else
        {
            _currentBiomass -= upgrade.Cost;

            switch (upgrade.Effect)
            {
                case UpgradeEffect.AddAttackStrength:
                    _attackStrength += upgrade.Amount;
                    break;
                case UpgradeEffect.AddBiomassGainMultiplier:
                    _biomassMultiplier += upgrade.Amount;
                    break;
                case UpgradeEffect.AddMaxTentacleLaunchVelocityStrength:
                    _maxTentacleLaunchStrength += upgrade.Amount;
                    break;
                case UpgradeEffect.AddMaxTentacles:
                    _maxTentacles += (int)upgrade.Amount;
                    break;
                case UpgradeEffect.EnablePoisonDartAttack:
                    _canPoisonDart = true;
                    break;
                case UpgradeEffect.EnableLaserBeamAttack:
                    _canLaserBeam = true;
                    break;
                case UpgradeEffect.EnableInkPouch:
                    _canInkPouch = true;
                    break;
            }

            EventManager.TriggerEvent("SuccessfullyBuyUpgrade", upgrade);
        }
    }
}
