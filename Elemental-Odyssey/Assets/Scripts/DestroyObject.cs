using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    public GameObject player;
    public bool sceneChange = false;
    void Start()
    {
        if(player) player.SetActive(true);
        Destroy(this.gameObject);
        if(sceneChange) SceneTransitionManager.Instance.SwitchScene(targetSceneName);
    }
}
