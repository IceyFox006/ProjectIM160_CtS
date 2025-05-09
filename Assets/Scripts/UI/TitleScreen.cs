/*
 * TitleScreen.cs
 * Marlow Greenan
 * 3/27/2025
 * 
 * Buttons and functions for the Title Screen.
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private string _gameSceneName;
    [SerializeField] private Settings _settingsCanvas;
    public void StartGame()
    {
        StaticData.CurrentLevel = 0;
        SceneManager.LoadScene(_gameSceneName);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        //if (UnityEditor.EditorApplication.isPlaying)
        //    UnityEditor.EditorApplication.isPlaying = false;
    }

    /// <summary>
    /// Opens settings menu.
    /// </summary>
    public void OpenSettings()
    {
        if (!_settingsCanvas.transform.GetChild(0).gameObject.activeSelf)
            _settingsCanvas.transform.GetChild(0).gameObject.SetActive(true);
        else
            _settingsCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Credits()
    {
        StaticData.End = Enums.End.None;
        SceneManager.LoadScene("Credits");
    }
}
