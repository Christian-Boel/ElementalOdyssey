using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameObject _player; // Reference til spillerobjektet
    public GameObject _playerPrefab; // Prefab til spilleren

    private string _currentSceneName; // Navn på den aktuelle scene
    private PlayerStats _playerStats; // Reference til spillerens stats
    private HashSet<KeyType> _collectedKeys = new HashSet<KeyType>(); // Samlede nøgler

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sørg for, at GameManager er persistent
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentSceneName = SceneManager.GetActiveScene().name;

        if (!_player && !_currentSceneName.ToLower().Contains("menu"))
        {
            SpawnPlayer();
        }

        _playerStats = gameObject.GetComponent<PlayerStats>();
    }

    public void SpawnPlayer()
    {
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawnPoint");

        if (_player)
        {
            return;
        }
        
        if (spawnPoint != null)
        {
            _player = Instantiate(_playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {
            Debug.LogWarning("PlayerSpawnPoint not found in the scene!");
            _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }


    public void HandlePickup(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.HealthPotion:
                HandleHealthPotion(item);
                break;
            case ItemType.Key:
                HandleKey(item);
                break;
            case ItemType.Upgrade:
                HandleUpgrade(item);
                break;
            default:
                Debug.LogWarning("Unhandled item type: " + item.itemType);
                break;
        }
        _playerStats.updateUI();
    }

    private void HandleHealthPotion(Item item)
    {
        if (_playerStats != null)
        {
            _playerStats.Heal(item.healAmount);
            Debug.Log("Picked up a health potion. Healed for " + item.healAmount);
        }
    }

    private void HandleKey(Item item)
    {
        KeyType keyType = item.keyType;
        AddKey(keyType);
        Debug.Log("Picked up a key: " + keyType);
    }

    private void HandleUpgrade(Item item)
    {
        if (_player != null)
        {
            Upgrade upgrade = item.upgrade; 
            Movement movement = _player.GetComponent<Movement>();
            PlayerStats stats = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<PlayerStats>();
            if (movement != null)
            {
                switch (upgrade.upgradeType)
                {
                    case UpgradeType.MovementSpeed:
                        stats.MS += upgrade.value;
                        break;

                    case UpgradeType.DashCooldown:
                        stats.dashCD -= upgrade.value; // Reduce cooldown
                        break;
            
                    case UpgradeType.DashLength:
                        stats.dashLength += upgrade.value; //increase dash length
                        break;
                }
                movement.UpdateStats();
                Debug.Log($"Applied upgrade: {upgrade.upgradeType} with value {upgrade.value}");
            }
            
            // Check for Attack upgrades
            Attack attack = _player.GetComponent<Attack>();
            if (attack != null && upgrade.upgradeType == UpgradeType.AttackDamage)
            {
                stats.AD += upgrade.value;
                attack.UpdateStats();
                return;
            }
        }
    }
    
    public void AddKey(KeyType keyType)
    {
        if (_collectedKeys.Add(keyType))
        {
            Debug.Log("Key added to inventory: " + keyType);
        }
        else
        {
            Debug.Log("Key already in inventory: " + keyType);
        }
    }

    public bool HasKey(KeyType keyType)
    {
        return _collectedKeys.Contains(keyType);
    }
    
}
