using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelProgressManager : MonoBehaviour
{
    public Button[] levelButtons;
    private const string PROGRESS_KEY = "LevelProgress";
    
    void Start()
    {
        LoadLevelProgress();
    }

    public void LoadLevelProgress()
    {
        int unlockedLevel = PlayerPrefs.GetInt(PROGRESS_KEY, 1);
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = (i + 1) <= unlockedLevel;
            levelButtons[i].interactable = isUnlocked;
            
            // Setup teks dan warna tombol
            var buttonText = levelButtons[i].GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Level " + (i + 1);
                buttonText.color = isUnlocked ? Color.white : Color.gray;
            }
        }
    }

    public static void UnlockNextLevel(int currentLevelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt(PROGRESS_KEY, 1);
        int nextLevel = currentLevelIndex + 1;
        
        if (nextLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt(PROGRESS_KEY, nextLevel);
            PlayerPrefs.Save();
            Debug.Log("Level " + nextLevel + " unlocked!");
        }
    }

    [ContextMenu("Reset Progress")]
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(PROGRESS_KEY);
        PlayerPrefs.Save();
        LoadLevelProgress();
        Debug.Log("Progress reset to Level 1");
    }
}