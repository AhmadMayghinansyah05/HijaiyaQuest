using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] public string[] levelNames; // Ubah menjadi public
    private bool[] levelCompletionStatus;

    private void Awake()
    {
        // Implementasi Singleton yang lebih robust
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeProgress();
            
            // Pastikan GameManager tetap ada saat load scene baru
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");
        // Tambahkan logika tambahan jika diperlukan
    }

    void InitializeProgress()
    {
        levelCompletionStatus = new bool[levelNames.Length];
        LoadProgress();
    }

    private void LoadProgress()
    {
        for (int i = 0; i < levelCompletionStatus.Length; i++)
        {
            levelCompletionStatus[i] = PlayerPrefs.GetInt("LevelComplete_" + i, 0) == 1;
            Debug.Log($"Level {levelNames[i]} status: {levelCompletionStatus[i]}");
        }
    }

    // Tambahkan method CanAccessLevel untuk kompatibilitas
    public bool CanAccessLevel(int levelIndex)
    {
        return IsLevelUnlocked(levelIndex);
    }

   public void CompleteCurrentLevel()
{
    string currentScene = SceneManager.GetActiveScene().name;
    Debug.Log($"Mencoba menyelesaikan level: {currentScene}");

    for (int i = 0; i < levelNames.Length; i++)
    {
        if (levelNames[i] == currentScene)
        {
            Debug.Log($"Level {i} ({currentScene}) berhasil diselesaikan");
            SetLevelComplete(i, true);
            
            // Unlock level berikutnya
            if (i < levelNames.Length - 1)
            {
                SetLevelComplete(i + 1, true);
                Debug.Log($"Level {i+1} ({levelNames[i+1]}) dibuka");
            }
            return;
        }
    }
    Debug.LogError($"Nama scene {currentScene} tidak ditemukan di levelNames!");
}

    public void SetLevelComplete(int levelIndex, bool isComplete)
{
    levelCompletionStatus[levelIndex] = isComplete;
    PlayerPrefs.SetInt("LevelComplete_" + levelIndex, isComplete ? 1 : 0);
    PlayerPrefs.Save(); // Ini penting!
    Debug.Log($"Progress level {levelIndex} disimpan: {isComplete}");
}

    // Ganti nama menjadi IsLevelUnlocked untuk lebih deskriptif
    public bool IsLevelUnlocked(int levelIndex)
    {
        // Level 1 selalu terbuka
        if (levelIndex == 0) return true;
        
        // Level lain terbuka jika level sebelumnya selesai
        return levelCompletionStatus[levelIndex - 1];
    }

    [ContextMenu("Reset All Progress")]
    public void ResetProgress()
    {
        for (int i = 0; i < levelCompletionStatus.Length; i++)
        {
            PlayerPrefs.DeleteKey("LevelComplete_" + i);
            levelCompletionStatus[i] = false;
        }
        PlayerPrefs.Save();
        Debug.Log("All progress reset");
    }

    // Method baru untuk mendapatkan nama level berdasarkan index
    public string GetLevelName(int index)
    {
        if (index >= 0 && index < levelNames.Length)
            return levelNames[index];
        return "";
    }
}