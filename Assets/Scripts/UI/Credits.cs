/*
 * Credits.cs
 * Marlow Greenan
 * 4/21/2025
 * 
 * Credits scene controller.
 */
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private TMP_Text daysLeftText;

    /// <summary>
    /// Plays credit roll and sets text to match ending.
    /// </summary>
    void Start()
    {
        GetComponent<Animator>().Play("ROLL");

        switch (StaticData.End)
        {
            case Enums.End.Loop:
                daysLeftText.text = "4 days left.";
                break;
            case Enums.End.EliotLeave:
                daysLeftText.text = "0 days left.";
                break;
            case Enums.End.Stay:
                daysLeftText.text = "2147483647 days left.";
                break;
        }
    }

    /// <summary>
    /// Return to title screen.
    /// </summary>
    public void Button_TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
