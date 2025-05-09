using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput.currentActionMap.Enable();
    }
    
    public void OnRESET(InputValue inputValue)
    {
        if (SceneManager.GetActiveScene().name == "TheWoods")
        {
            StaticData.ManualReset = false;
            LevelManager.Instance.LoadCharacterPoints();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            LevelManager.Instance.ResetLevel(LevelManager.Instance.Levels[StaticData.CurrentLevel]);
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnQUIT(InputValue inputValue)
    {
        Debug.Log("QUIT");
        Application.Quit();

        //GameManager.Instance.Quit();
    }
}
