using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenej : MonoBehaviour
{

    public int levelLoad;
    public GameObject gos1, gos2;

    public void BukaHalamanUtama(string url)
    {
        Application.OpenURL(url);
    }

    public void MuatLevel(int loadLevel)
    {
        SceneManager.LoadScene(loadLevel);
    }

    public void Keluar()
    {
        Application.Quit();
    }
	
	public void Ulangi()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    Time.timeScale = 1;
	}
	
	public void Pause()
	{
		Time.timeScale = 0;
	}
	
	public void Resume()
	{
		Time.timeScale = 1;
	}

  public void GamePlay()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void MainMenu() 
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }

}
