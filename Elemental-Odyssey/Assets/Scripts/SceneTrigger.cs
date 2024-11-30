using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    private GameObject gameObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player GameObject has the "Player" tag
        {
            SceneTransitionManager.Instance.SwitchScene(targetSceneName);
        }
    }
}