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

    [SerializeField]
    private Transform _spawnParent;
    public Transform SpawnParent => _spawnParent;
    [SerializeField]
    private Transform _sunObject;
    [SerializeField]
    private Sprite _sunSprite;
    [SerializeField]
    private Sprite _moonSprite;
    [SerializeField]
    private SpriteRenderer _skyObject;
    [SerializeField]
    private Light _lightObject;
    [SerializeField]
    private Vector3 _lightDayRotation;
    [SerializeField]
    private Vector3 _lightNightRotation;
    [SerializeField]
    private Sprite _skyDaySprite;
    [SerializeField]
    private Sprite _skyNightSprite;
    [SerializeField]
    private Color _waterDayColor;
    [SerializeField]
    private Color _waterNightColor;
    [SerializeField]
    private Transform _player;


    [SerializeField]
    private AudioClip _playerHurtClip;

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
    public float AwarenessMult => Mathf.Max(0.1f, (100 - _currentAwareness) / 100);

    //Upgradables
    private float _attackStrength = 1f;
    public float AttackStrength => _attackStrength;

    private float _attackCooldown = 5f;
    public float AttackCooldown => _attackCooldown;

    private float _biomassMultiplier = 1f;
    public float BiomassMultipier => _biomassMultiplier;
    private float _maxTentacleLaunchStrength = 10f;
    public float MaxTentacleLaunchStrength => _maxTentacleLaunchStrength;
    private int _maxTentacles = 3;
    public int MaxTentacles => _maxTentacles;
    private float _tentactleStrength = 1f;
    public float TentacleStrength => _tentactleStrength;

    private float _tentacleVelocityScale = 4f;
    public float TentacleVelocityScale => _tentacleVelocityScale;
    private bool _canPoisonDart = false;
    public bool CanPoisonDart => _canPoisonDart;
    private bool _canLaserBeam = false;
    public bool CanLaserBeam => _canLaserBeam;
    private bool _canInkCloud = false;
    public bool CanInkCloud => _canInkCloud;

    private float _inkCloudCooldown = 30f;
    public float InkCloudCooldown => _inkCloudCooldown;
    private bool _inkCloudActive;
    private bool _canSirenSong = false;
    public bool CanSirenSong => _canSirenSong;
    public bool InkCloudActive => _sirenSongActive;
    private float _sirenSongCooldown = 30f;
    public float SirenSongCooldown => _sirenSongCooldown;
    private bool _sirenSongActive;
    public bool SirenSongActive => _sirenSongActive;



    public event Action FinishDay;
    public event Action NewDay;
    public event Action LoseGame;
    public event Action WinGame;


    private void OnEnable()
    {
        EventManager.AddListener("SelectUpgrade", _OnSelectUpgrade);
        EventManager.AddListener("IncreaseAwareness", _OnIncreaseAwareness);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("SelectUpgrade", _OnSelectUpgrade);
        EventManager.RemoveListener("IncreaseAwareness", _OnIncreaseAwareness);
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

        _dayTimer += Time.deltaTime;
        _UpdateSunObject();
        if (!_isNight && _dayTimer >= Globals.DAY_DURATION) _StartNight();
        if (_dayTimer >= Globals.DAY_TOTAL_DURATION) _EndDay();

        if (Globals.DAY_TOTAL_DURATION - _dayTimer <= 1) return;

        if (!_isNight && _currentDayData.DayEnemiesToSpawn.Length > 0
            || _isNight && _currentDayData.NightEnemiesToSpawn.Length > 0)
        {
            _CheckEnemySpawn();
        }
        if (!_isNight && _currentDayData.DayObstaclesToSpawn.Length > 0
            || _isNight && _currentDayData.NightObstaclesToSpawn.Length > 0)
        {
            _CheckObstacleSpawn();
        }
        if (!_isNight && _currentDayData.DayVictimsToSpawn.Length > 0
            || _isNight && _currentDayData.NightVictimsToSpawn.Length > 0)
        {
            _CheckVictimSpawn();
        }
    }

    private void _CheckEnemySpawn()
    {
        _spawnEnemyTimer += Time.deltaTime;
        if (_spawnEnemyTimer >= _nextEnemySpawn)
        {
            EnemyData enemy = _isNight ? _currentDayData.NightEnemiesToSpawn[UnityEngine.Random.Range(0, _currentDayData.NightEnemiesToSpawn.Length)] :
                _currentDayData.DayEnemiesToSpawn[UnityEngine.Random.Range(0, _currentDayData.DayEnemiesToSpawn.Length)];
            _SpawnEnemy(enemy);
            _spawnEnemyTimer = 0f;
            _nextEnemySpawn = UnityEngine.Random.Range(Mathf.Max(3, (Globals.MIN_TIME_BETWEEN_ENEMY_SPAWNS + _currentDayData.SpawnCooldown) * AwarenessMult),
                Mathf.Max(3, (Globals.MAX_TIME_BETWEEN_ENEMY_SPAWNS + _currentDayData.SpawnCooldown) * AwarenessMult));
            _nextEnemySpawn *= _currentDayData.SpawnEnemyTimeMuliplier;
        }
    }

    private void _CheckObstacleSpawn()
    {
        _spawnObstacleTimer += Time.deltaTime;
        if (_spawnObstacleTimer >= _nextObstacleSpawn)
        {
            ObstacleData obstacle = _isNight ? _currentDayData.NightObstaclesToSpawn[UnityEngine.Random.Range(0, _currentDayData.NightObstaclesToSpawn.Length)] :
                _currentDayData.DayObstaclesToSpawn[UnityEngine.Random.Range(0, _currentDayData.DayObstaclesToSpawn.Length)];
            _SpawnObstacle(obstacle);
            _spawnObstacleTimer = 0f;
            _nextObstacleSpawn = UnityEngine.Random.Range(Mathf.Max(3, Globals.MIN_TIME_BETWEEN_OBSTACLE_SPAWNS + _currentDayData.SpawnCooldown * AwarenessMult),
                Mathf.Max(3, Globals.MAX_TIME_BETWEEN_OBSTACLE_SPAWNS + _currentDayData.SpawnCooldown) * AwarenessMult);
            _nextObstacleSpawn *= _currentDayData.SpawnObstacleTimeMultiplier;
        }
    }

    private void _CheckVictimSpawn()
    {
        _spawnVictimTimer += Time.deltaTime;
        if (_spawnVictimTimer >= _nextVictimSpawn)
        {
            VictimData victim = _isNight ? _currentDayData.NightVictimsToSpawn[UnityEngine.Random.Range(0, _currentDayData.NightVictimsToSpawn.Length)] :
                _currentDayData.DayVictimsToSpawn[UnityEngine.Random.Range(0, _currentDayData.DayVictimsToSpawn.Length)];
            _SpawnVictim(victim);
            _spawnVictimTimer = 0f;
            _nextVictimSpawn = UnityEngine.Random.Range(Globals.MIN_TIME_BETWEEN_VICTIM_SPAWNS + _currentDayData.SpawnCooldown, Globals.MAX_TIME_BETWEEN_VICTIM_SPAWNS + _currentDayData.SpawnCooldown);
        }
    }

    public void _SelectNextDay()
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
        _isNight = false;
        _sunObject.GetComponent<SpriteRenderer>().sprite = _sunSprite;
        _skyObject.sprite = _skyDaySprite;
        _lightObject.transform.localEulerAngles = _lightDayRotation;
        Camera.main.backgroundColor = _waterDayColor;
    }

    private void _UpdateSunObject()
    {
        float distancePercent;

        if (!_isNight)
        {
            distancePercent = _dayTimer / Globals.DAY_DURATION;
        }
        else
        {
            distancePercent = (_dayTimer - Globals.DAY_DURATION) / Globals.NIGHT_DURATION;

        }

        _sunObject.position = new(-10f + (distancePercent * 20f), Globals.SUN_OBJECT_Y);
    }

    private void _StartNight()
    {
        _isNight = true;
        _skyObject.sprite = _skyNightSprite;
        _lightObject.transform.localEulerAngles = _lightNightRotation;
        Camera.main.backgroundColor = _waterNightColor;
        _sunObject.GetComponent<SpriteRenderer>().sprite = _moonSprite;
    }

    private void _EndDay()
    {
        SetPauseGame(true);

        if (_currentDay >= 10 && _currentBiomass >= _currentDayData.RequiredBiomass)
        {
            WinGame?.Invoke();
        }
        else if (_currentBiomass >= _currentDayData.RequiredBiomass)
        {
            FinishDay?.Invoke();
        }
        else
        {
            LoseGame?.Invoke();
        }

        foreach (Transform child in _spawnParent)
        {
            Destroy(child.gameObject);
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

        if (_currentBiomass < upgrade.Cost)
        {
            Debug.Log("Too Poor");
        }
        else
        {
            _currentBiomass -= upgrade.Cost;
            EventManager.TriggerEvent("UpdateBiomass", _currentBiomass);

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
                case UpgradeEffect.EnableInkCloud:
                    _canInkCloud = true;
                    _player.GetComponent<InkCloud>().ActivateManager();
                    break;
                case UpgradeEffect.EnableSirenSong:
                    _canSirenSong = true;
                    _player.GetComponent<SirenSong>().ActivateManager();
                    break;
            }

            EventManager.TriggerEvent("SuccessfullyBuyUpgrade", upgrade);
        }
    }

    public void PlayerDefeatOccupant(OccupantManager occupant)
    {
        AddBiomass(occupant.data.Biomass);
    }

    public void PlayerTakeDamage(int amount)
    {
        AddBiomass(-amount);
        AudioManager.Instance.source.PlayOneShot(_playerHurtClip);
        if (_currentBiomass <= 0)
        {
            LoseGame?.Invoke();
        }
    }

    private void _OnIncreaseAwareness(object data)
    {
        if (_isNight || _inkCloudActive) return;

        int amount = (int)data;

        _currentAwareness += amount;
        EventManager.TriggerEvent("UpdateAwareness", _currentAwareness);
    }

    public void AddBiomass(int amount)
    {
        _currentBiomass += amount;
        EventManager.TriggerEvent("UpdateBiomass", _currentBiomass);
    }

    public void StartNextDay()
    {
        _SelectNextDay();
        SetPauseGame(false);
        NewDay?.Invoke();
    }

    public void SetInkCloudActive(bool isActive)
    {
        _inkCloudActive = isActive;
    }

    public void SetSirenSongActive(bool isActive)
    {
        _sirenSongActive = isActive;
    }

    public void SetPauseGame(bool paused)
    {
        _gameIsPaused = paused;

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
