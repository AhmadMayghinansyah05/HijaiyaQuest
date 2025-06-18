using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishPanel : MonoBehaviour
{
    public static FinishPanel Instance;
    
    public GameObject panel;
    public TMP_Text resultText;
    public Button nextLevelButton;
    public Button retryButton;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        panel.SetActive(false);
    }

    public void Show(bool allCorrect, int currentLevelIndex)
    {
        if (allCorrect)
        {
            resultText.text = "Selamat! Semua jawaban benar!";
            nextLevelButton.interactable = GameManager.Instance.CanAccessLevel(currentLevelIndex + 1);
        }
        else
        {
            resultText.text = "Masih ada jawaban yang salah!";
            nextLevelButton.interactable = false;
        }
        
        panel.SetActive(true);
    }

    public void OnNextLevelPressed()
    {
        // Implementasi pindah ke level berikutnya
        panel.SetActive(false);
    }

    public void OnRetryPressed()
    {
        // Implementasi mengulang level
        panel.SetActive(false);
    }
}