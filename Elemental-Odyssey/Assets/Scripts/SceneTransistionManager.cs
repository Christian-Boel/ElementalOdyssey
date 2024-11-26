using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    private bool _isTransitioning = false;
    
    public FadeOutCutscene fadeOutCutscene;

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

    // Switch to a new scene
    public void SwitchScene(string sceneName)
    {
        if (!_isTransitioning)
        {
            fadeOutCutscene.fade = true;
            StartCoroutine(TransitionToScene(sceneName));
        }
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        _isTransitioning = true;

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (!fadeOutCutscene) fadeOutCutscene = GameObject.Find("BlackoutCurtain").GetComponent<FadeOutCutscene>();
        _isTransitioning = false;
    }
}