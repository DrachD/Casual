using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

////////////////////////////////////////////////////////
// class for managing scenes
// switch between scenes
////////////////////////////////////////////////////////
public class SceneController : MonoBehaviour
{
    [SerializeField] LevelData _levelData;

    private MenuPaused _menuPaused;

    private SoundManager _soundManager;

    private void Awake()
    {
        _menuPaused = GameObject.Find("MenuManager").GetComponent<MenuPaused>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public  void LoadNextLevel()
    {
        if (_levelData.selectedLevel + 1 <= _levelData.maxLevels)
        {

            _levelData.selectedLevel++;
            SceneManager.LoadScene(_levelData.selectedLevel);
        }
    }

    public void RestartLevel()
    {
        _menuPaused?.NoPause();
        SceneManager.LoadScene(_levelData.selectedLevel, LoadSceneMode.Single);
    }

    public void LoadLevel(int level)
    {
        _menuPaused?.NoPause();
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
    
    public void LoadMainMenu()
    {
        _menuPaused?.NoPause();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
