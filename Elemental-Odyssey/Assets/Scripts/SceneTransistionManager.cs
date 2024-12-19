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

    public IEnumerator TransitionToScene(string sceneName)
    {
        _isTransitioning = true;

        // Fade out effect before scene transition
        if (fadeOutCutscene != null)
        {
            yield return new WaitUntil(() => fadeOutCutscene.IsFadeComplete());
        }

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Wait for the new scene to finish loading
        yield return new WaitForSeconds(.1f);
        
        //SpawnPlayer
        if(sceneName != "Main Menu")
        {
            GameManager.Instance.SpawnPlayer();

            GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<PlayerStats>().NewScene();
            fadeOutCutscene = GameObject.Find("BlackoutCurtain").GetComponent<FadeOutCutscene>();
        }
            
        

        // Update player position in the new scene
        //UpdatePlayerPosition();

        _isTransitioning = false;
    }
}
