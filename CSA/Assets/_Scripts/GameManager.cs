using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int levelToLoad;
    public static ScreenFader screennFader;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadLevel()
    {
        screennFader.StartFadeOut();
        // You might want to delay loading the next scene until the fade out is complete.
        StartCoroutine(LoadNextSceneAfterFade());
    }

    public void LoadNextLevel()
    {
        levelToLoad = (levelToLoad + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(levelToLoad);
    }

    public void LoadPreviousLevel()
    {
        levelToLoad = (levelToLoad - 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(levelToLoad);
    }

    private IEnumerator LoadNextSceneAfterFade()
    {
        while (ScreenFader.isFading)
        {
            yield return null;
        }

        LoadNextLevel();
    }


}
