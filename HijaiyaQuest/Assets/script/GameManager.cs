using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private LevelData[] allLevels;
    private int currentLevelIndex;
    private bool[] levelCompletionStatus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Inisialisasi status penyelesaian level
            levelCompletionStatus = new bool[allLevels.Length];
            for (int i = 0; i < levelCompletionStatus.Length; i++)
            {
                levelCompletionStatus[i] = false;
            }
            
            // Load progress yang disimpan (jika ada)
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevelComplete(int levelIndex, bool isComplete)
    {
        if (levelIndex >= 0 && levelIndex < levelCompletionStatus.Length)
        {
            levelCompletionStatus[levelIndex] = isComplete;
            SaveProgress();
        }
    }

    public bool IsLevelComplete(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelCompletionStatus.Length)
            return levelCompletionStatus[levelIndex];
        return false;
    }

    public bool CanAccessLevel(int levelIndex)
    {
        // Level 0 selalu bisa diakses
        if (levelIndex == 0) return true;
        
        // Level selanjutnya hanya bisa diakses jika level sebelumnya sudah selesai
        return IsLevelComplete(levelIndex - 1);
    }

    private void SaveProgress()
    {
        // Implementasi penyimpanan progress (PlayerPrefs, file save, dll)
        for (int i = 0; i < levelCompletionStatus.Length; i++)
        {
            PlayerPrefs.SetInt("LevelComplete_" + i, levelCompletionStatus[i] ? 1 : 0);
        }
    }

    private void LoadProgress()
    {
        for (int i = 0; i < levelCompletionStatus.Length; i++)
        {
            levelCompletionStatus[i] = PlayerPrefs.GetInt("LevelComplete_" + i, 0) == 1;
        }
    }
}