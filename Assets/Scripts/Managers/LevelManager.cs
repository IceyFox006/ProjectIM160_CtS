/* 
 * LevelManager.cs
 * Marlow Greenan
 * 3/23/2025
 */
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    [SerializeField] private GameObject _levelLoadScreen;
    [SerializeField] private float _loadTime;
    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] private List<GameObject> _levelBoundaries = new List<GameObject>();
    [SerializeField] private int _currentLevel = 0;

    private bool completedLevel = false;
    private int _progressionScore = 0;

    public int ProgressionScore { get => _progressionScore; set => _progressionScore = value; }
    public List<Level> Levels { get => _levels; set => _levels = value; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public static LevelManager Instance { get => instance; set => instance = value; }
    public bool CompletedLevel { get => completedLevel; set => completedLevel = value; }

    [SerializeField] private bool DebugMode = false;


    private void Start()
    {
        instance = this;

        if (StaticData.ManualReset)
        {
            SaveCharacterPoints();
            ResetCharacters(GameManager.Instance.Characters);
        }

        if (!DebugMode)
            _currentLevel = StaticData.CurrentLevel;
        if (_currentLevel > -1 && _currentLevel < _levels.Count)
            LoadLevel();
    }

    public void LoadLevel()
    {
        StaticData.CurrentLevel = _currentLevel;
        _levelLoadScreen.GetComponentInChildren<TMP_Text>().text = (_levels.Count - CurrentLevel).ToString() + " days left.";
        _levelLoadScreen.GetComponent<Animator>().Play("DISABLE", 0, 0f);
        //StartCoroutine(GameManager.Instance.DialogueManager.TypeText(_levelLoadScreen.GetComponentInChildren<TMP_Text>(), (_levels.Count - CurrentLevel).ToString() + " days left."));
        InstantLoadLevel();
    }

    /// <summary>
    /// Instantly loads level.
    /// </summary>
    public void InstantLoadLevel()
    {
        completedLevel = false;
        GameManager.Instance.PlayerAvatar.transform.position = Vector3.zero;
        for (int index = 0; index < _levels.Count; index++)
        {
            if (index <= CurrentLevel)
                _levelBoundaries[index].SetActive(false);
            else
                _levelBoundaries[index].SetActive(true);
        }
        EnableOnRequirement.SetActiveOnRequirement();

        if (_levels[CurrentLevel].WorldSound != null)
            GameManager.Instance.WorldAudio.clip = _levels[CurrentLevel].WorldSound;
        GameManager.Instance.WorldAudio.Play();
        RenderSettings.fogDensity = _levels[CurrentLevel].FogDensity;

        GameManager.Instance.DialogueBlackScreen.GetComponent<Animator>().Play("IDLE");

        GameManager.Instance.HUD.DisableHUD(true);
        GameManager.Instance.DialogueManager.StartDialogue(_levels[_currentLevel].IntroDialogue);
    }

    /// <summary>
    /// Updates level progress.
    /// </summary>
    /// <param name="progressionValue"></param>
    public void UpdateProgression(int progressionValue = 1)
    {
        _progressionScore += progressionValue;
        if (_progressionScore >= _levels[_currentLevel].CompletetionScore)
            CompleteLevel();
    }

    /// <summary>
    /// Plays outro dialogue and progresses to next level.
    /// </summary>
    public void CompleteLevel()
    {
        completedLevel= true;
        GameManager.Instance.DialogueManager.StartDialogue(_levels[_currentLevel].OutroDialogue);
    }


    public void SaveCharacterPoints()
    {
        StaticData.CharacterPointSaves.Clear();
        for (int index = 0; index < GameManager.Instance.Characters.Length; index++)
        {
            StaticData.CharacterPointSaves.Add(GameManager.Instance.Characters[index].RelationshipScore);
        }
    }
    public void LoadCharacterPoints()
    {
        Debug.Log("LOAD");
        for (int index = 0; index < GameManager.Instance.Characters.Length; index++)
        {
            GameManager.Instance.Characters[index].RelationshipScore = StaticData.CharacterPointSaves[index];
        }
    }

    /// <summary>
    /// Checks quest progression and plays intro and outro dialogue accordingly.
    /// </summary>
    /// <param name="quest"></param>
    public void CheckQuestProgress(Quest quest)
    {
        int completedTasks = 0;
        foreach (Task task in quest.Tasks)
        {
            if (task != null && task.IsComplete)
                completedTasks++;
        }
        if (completedTasks <= 0 && !quest.HasPlayedIntroText)
        {
            GameManager.Instance.DialogueManager.StartDialogue(quest.IntroDialgoue);
            quest.HasPlayedIntroText = true;
        }
        if (completedTasks >= quest.Tasks.Count && !quest.HasPlayedOutroText)
        {
            GameManager.Instance.DialogueManager.StartDialogue(quest.OutroDialgoue);
            quest.HasPlayedOutroText = true;
        }
    }

    /// <summary>
    /// Resets level data.
    /// </summary>
    public void ResetLevel(Level level)
    {
        ResetQuest(level.SideQuest);
        _progressionScore = 0;
    }

    /// <summary>
    /// Reset quest data.
    /// </summary>
    public void ResetQuest(Quest quest)
    {
        quest.IsActive = false;
        quest.HasPlayedIntroText = false;
        quest.HasPlayedOutroText = false;
        for (int index = 0; index < quest.Tasks.Count; index++)
        {
            if (quest.Tasks[index] != null)
                ResetTask(quest.Tasks[index]);
        }
    }

    /// <summary>
    /// Reset task data.
    /// </summary>
    public void ResetTask(Task task)
    {
        task.IsComplete = false;
    }

    /// <summary>
    /// Resets character relationship scores.
    /// </summary>
    /// <param name="characters"></param>
    public void ResetCharacters(Character[] characters)
    {
        foreach (Character character in characters)
        {
            character.RelationshipScore = 0;
        }
    }
}
