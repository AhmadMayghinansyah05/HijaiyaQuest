using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button retryButton;

    [Header("Messages")]
    [SerializeField] private string completeMessage = "LEVEL COMPLETE!";
    [SerializeField] private string incompleteMessage = "Complete all questions first!";
    [SerializeField] private string mainMenuScene = "MainMenu";

    [Header("Question Tracking")]
    [SerializeField] private SoalTrigger[] allSoalTriggers;
    [SerializeField] private bool autoFindQuestions = true;

    private bool allCorrect = false;

    private void Awake()
    {
        NullCheckValidation();
        InitializeQuestionTriggers();
        SetupButtonListeners();
    }

    private void NullCheckValidation()
    {
        if (finishPanel == null)
            Debug.LogError("Finish Panel belum di-assign!", this);
        
        if (statusText == null && finishPanel != null)
            statusText = finishPanel.GetComponentInChildren<TMP_Text>();
        
        if (allSoalTriggers == null || allSoalTriggers.Length == 0)
            Debug.LogWarning("Daftar soal belum di-assign", this);
    }

    private void InitializeQuestionTriggers()
    {
        if (autoFindQuestions && (allSoalTriggers == null || allSoalTriggers.Length == 0))
        {
            allSoalTriggers = FindObjectsOfType<SoalTrigger>();
            Debug.Log($"Auto-detect menemukan {allSoalTriggers.Length} soal");
        }
    }

    private void SetupButtonListeners()
    {
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        else
            Debug.LogError("Next Level Button belum di-assign!", this);
        
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(LoadMainMenu);
        
        if (retryButton != null)
            retryButton.onClick.AddListener(RetryLevel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckAllQuestions();
            ShowFinishPanel();
        }
    }

    private void CheckAllQuestions()
    {
        allCorrect = true;

        if (allSoalTriggers == null || allSoalTriggers.Length == 0)
        {
            Debug.LogWarning("Tidak ada soal yang perlu dicek");
            return;
        }

        foreach (var soal in allSoalTriggers)
        {
            if (soal == null)
            {
                Debug.LogWarning("Ditemukan soal trigger yang null");
                allCorrect = false;
                continue;
            }

            if (!soal.IsAnsweredCorrectly())
            {
                allCorrect = false;
                break;
            }
        }
    }

    private void ShowFinishPanel()
    {
        if (finishPanel == null)
        {
            Debug.LogError("Tidak dapat menampilkan panel - finishPanel null");
            return;
        }

        finishPanel.SetActive(true);

        if (statusText != null)
            statusText.text = allCorrect ? completeMessage : incompleteMessage;

        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(allCorrect);

        if (retryButton != null)
            retryButton.gameObject.SetActive(!allCorrect);

        if (allCorrect && GameManager.Instance != null)
        {
            GameManager.Instance.CompleteCurrentLevel();
        }

}

    private void LoadNextLevel()
{
    // Try to get the next level name
    string nextLevel = "";
    string currentScene = SceneManager.GetActiveScene().name;

    if (currentScene.StartsWith("Level"))
    {
        if (int.TryParse(currentScene.Replace("Level", ""), out int currentLevel))
        {
            nextLevel = "Level" + (currentLevel + 1);
        }
    }

    // If we have a GameManager instance, use it
    if (GameManager.Instance != null)
    {
        GameManager.Instance.CompleteCurrentLevel();

        // If we found a next level, try to load it
        if (!string.IsNullOrEmpty(nextLevel) && Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
            return;
        }
    }
    // If no GameManager but we have a valid next level
    else if (!string.IsNullOrEmpty(nextLevel) && Application.CanStreamedLevelBeLoaded(nextLevel))
    {
        SceneManager.LoadScene(nextLevel);
        return;
    }

    // Fallback to main menu
    SceneManager.LoadScene(mainMenuScene);
}   

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    private void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}