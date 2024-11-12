using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //private EnemyManager enemyManager;
    private AudioManager audioManager;
    //private UIManager uiManager;
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
        //enemyManager = GetComponent<EnemyManager>();
        audioManager = GetComponent<AudioManager>();
        //uiManager = GetComponent<UIManager>();
        sceneTransitionManager = GetComponent<SceneTransitionManager>();
    }

    public void SpawnEnemy()
    {
       // enemyManager.SpawnEnemies();
    }

    public void SwitchScene(string sceneName)
    {
        sceneTransitionManager.SwitchScene(sceneName);
    }

    // Other global methods...
}
