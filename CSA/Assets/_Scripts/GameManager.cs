using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int levelToLoad;
    public static ScreenFader screenFader;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #region Level Manager


    public void LoadLevel()
    {
        screenFader.StartFadeOut();
        // You might want to delay loading the next scene until the fade out is complete.
        StartCoroutine(LoadNextSceneAfterFade());
    }

    public void LoadLevel(int _level)
    {
        SceneManager.LoadScene(_level);
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


    public void RestartLevel()
    {
#if UNITY_EDITOR
        levelToLoad = SceneManager.GetActiveScene().buildIndex;
#endif
        // Reload the currently active scene.
        SceneManager.LoadScene(levelToLoad);
    }

    #endregion

    public void Quit()
    {
        Application.Quit();
    }
}

