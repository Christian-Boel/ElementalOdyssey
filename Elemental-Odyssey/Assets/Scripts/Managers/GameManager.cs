using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private GameObject _player;
    public GameObject _playerPrefab;
    
    
    private string _currentSceneName;
    private bool _shouldSpawnPlayer = false;
    private PlayerStats _playerStats;
    private AudioManager audioManager;
    private SceneTransitionManager sceneTransitionManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        sceneTransitionManager = GetComponent<SceneTransitionManager>();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentSceneName = SceneManager.GetActiveScene().name;
        
        if (!_player && !_currentSceneName.ToLower().Contains("menu"))
        {
            _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        }
        
        _playerStats = _player.GetComponent<PlayerStats>();
        
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
            default:
                Debug.LogWarning("Unhandled item type: " + item.itemType);
                break;
        }
    }

    private void HandleHealthPotion(Item item)
    {
        int healAmount = item.healAmount;
        _playerStats.Heal(healAmount);
        Debug.Log("Picked up a health potion. Healed for " + healAmount);
    }

    private void HandleKey(Item item)
    {
        KeyType keyType = item.keyType;
        // playerInventory.AddKey(keyType);
        Debug.Log("Picked up a key: " + keyType);
    }
    
    public void SpawnEnemy()
    {
       // enemyManager.SpawnEnemies();
    }

    public void SwitchScene(string sceneName)
    {
        
        //TODO: SET PLAYER UP - RENDER PLAYER IF ITS NOT ALREADY DONE!!!!!!!!!
    
        sceneTransitionManager.SwitchScene(sceneName);
    }

    // Other global methods...
}
