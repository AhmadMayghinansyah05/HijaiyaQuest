using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons; // ButtonLevel1, ButtonLevel2, ButtonLevel3
    public Button buttonClear;

    void Start()
    {
        // Load progress saat awal
        UpdateLevelButtons();
        
        // Setup tombol clear progress
        if (buttonClear != null)
        {
            buttonClear.onClick.AddListener(ClearProgress);
        }
    }

    void UpdateLevelButtons()
    {
        int highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = (i + 1) <= highestUnlockedLevel;
            levelButtons[i].interactable = isUnlocked;
            
            // Update tampilan tombol
            var buttonText = levelButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.color = isUnlocked ? Color.white : Color.gray;
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if ((levelIndex) <= PlayerPrefs.GetInt("HighestUnlockedLevel", 1))
        {
            SceneManager.LoadScene("Level" + levelIndex);
        }
        else
        {
            Debug.Log("Level terkunci! Selesaikan level sebelumnya terlebih dahulu.");
        }
    }

    public void ClearProgress()
    {
        PlayerPrefs.DeleteKey("HighestUnlockedLevel");
        PlayerPrefs.Save();
        UpdateLevelButtons();
        Debug.Log("Progress direset - Hanya Level 1 yang terbuka");
    }

    // Dipanggil ketika menyelesaikan sebuah level
    public static void UnlockNextLevel(int completedLevel)
    {
        int currentUnlocked = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);
        if (completedLevel >= currentUnlocked)
        {
            PlayerPrefs.SetInt("HighestUnlockedLevel", completedLevel + 1);
            PlayerPrefs.Save();
        }
    }
}