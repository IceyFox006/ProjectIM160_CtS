/* 
 * TaskTrigger.cs
 * Marlow Greenan
 * 3/23/2025
 */
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField] private Task _task;
    [SerializeField] private Enums.InteractType _interactType;
    [SerializeField] private bool requireHoldAcorn = false;

    /// <summary>
    /// Triggers task on area enter.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider collision)
    {
        if (_interactType == Enums.InteractType.Trigger && collision.gameObject == GameManager.Instance.PlayerAvatar)
            TriggerTask();
    }

    /// <summary>
    /// Triggers class on mouse click.
    /// </summary>
    public void OnMouseDown()
    {
        if (_interactType == Enums.InteractType.Click)
            TriggerTask();
    }
    
    /// <summary>
    /// Triggers task.
    /// </summary>
    public void TriggerTask()
    {
        if (!GameManager.Instance.InUI) //If UI is closed.
        {
            bool isForCurrentQuest = false;
            foreach (Task task in LevelManager.Instance.Levels[LevelManager.Instance.CurrentLevel].SideQuest.Tasks)
            {
                if (task == _task) //If task is for current quest.
                {
                    isForCurrentQuest = true;
                    break;
                }
            }
            if (isForCurrentQuest)
            {
                bool prerequisiteComplete = true;
                if (_task.Prerequisites != null) //If task has prerequisite tasks.
                {
                    foreach (Task prerequisite in _task.Prerequisites)
                    {
                        if (!prerequisite.IsComplete) //If prerequisite is not complete.
                        {
                            prerequisiteComplete = false;
                            break;
                        }
                    }
                }
                if (prerequisiteComplete)
                {
                    bool holdingRequiredItem = false;
                    if (!requireHoldAcorn || (requireHoldAcorn && InventoryManager.CurrentHoldItemID == InventoryManager.Instance.Acorn))
                        holdingRequiredItem = true;
                    if (holdingRequiredItem)
                    {
                        if (LevelManager.Instance.Levels[LevelManager.Instance.CurrentLevel].SideQuest.IsActive) //If level sidequest is accepted.
                        {
                            if (!_task.IsComplete) //If task is not complete.
                            {
                                GameManager.Instance.Message.NewMessage("Quest updated.");
                                _task.IsComplete = true;
                                if (_task.Dialogue != null)
                                    GameManager.Instance.DialogueManager.StartDialogue(_task.Dialogue);
                            }
                            else
                            {
                                if (_task.RepeatDialogue != null)
                                    GameManager.Instance.DialogueManager.StartDialogue(_task.RepeatDialogue);
                            }
                        }
                        else if (_task.DeniedDialogue != null)
                            GameManager.Instance.DialogueManager.StartDialogue(_task.DeniedDialogue);
                    }
                }
                    
            }
            else
            {
                if (_task.UnactiveDialogue != null)
                    GameManager.Instance.DialogueManager.StartDialogue(_task.UnactiveDialogue);
            }
        }
    }
}
