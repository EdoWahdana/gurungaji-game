using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject loadingScreen,canvasmenu;
    public Slider slider;
    public int urutanscene;

    public void LoadMenu()
    {
        SceneManager.LoadScene(urutanscene);
    }

    public void LoadLevel(int SceneIndex)
    {
        StartCoroutine(LoadingScreen(SceneIndex));
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    IEnumerator LoadingScreen (int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
