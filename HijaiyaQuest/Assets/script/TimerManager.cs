using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float initialTime = 60f;
    [SerializeField] private float wrongAnswerPenalty = 5f;
    [SerializeField] private float hintPenalty = 3f;
    
    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;
    
    [Header("Visual Feedback")]
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] private float warningThreshold = 10f;
    [SerializeField] private float penaltyFlashDuration = 0.5f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip timePenaltySound;
    [SerializeField] private AudioClip timeOutSound;
    
    [Header("Player Reference")]
    [SerializeField] private PlayerFunction playerFunction;

    private float currentTime;
    private bool isRunning = true;
    private AudioSource audioSource;
    private Color originalColor;

    private void Start()
    {
        currentTime = initialTime;
        audioSource = GetComponent<AudioSource>();
        
        if (timerText != null)
        {
            originalColor = timerText.color;
        }
        
        // Cari player otomatis jika belum di-assign
        if (playerFunction == null)
        {
            playerFunction = FindObjectOfType<PlayerFunction>();
        }
        
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (currentTime <= 0f && isRunning)
        {
            currentTime = 0f;
            isRunning = false;
            OnTimeOut();
        }
    }

    private void OnTimeOut()
    {
        // Play sound effect
        if (audioSource != null && timeOutSound != null)
        {
            audioSource.PlayOneShot(timeOutSound);
        }
        
        // Panggil fungsi kematian player
        if (playerFunction != null)
        {
            playerFunction.Die("WAKTU SUDAH HABIS!");
        }
        else
        {
            Debug.LogError("PlayerFunction reference tidak ditemukan!");
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Visual feedback saat waktu hampir habis
            timerText.color = currentTime <= warningThreshold ? warningColor : originalColor;
        }
    }

    public void ApplyWrongAnswerPenalty()
    {
        ApplyTimePenalty(wrongAnswerPenalty);
    }

    public void ApplyHintPenalty()
    {
        ApplyTimePenalty(hintPenalty);
    }

    private void ApplyTimePenalty(float penalty)
    {
        currentTime -= penalty;
        if (currentTime < 0f) currentTime = 0f;
        
        // Feedback visual
        if (timerText != null)
        {
            StartCoroutine(FlashTimer());
        }
        
        // Feedback audio
        if (audioSource != null && timePenaltySound != null)
        {
            audioSource.PlayOneShot(timePenaltySound);
        }
        
        UpdateTimerDisplay();
    }

    private System.Collections.IEnumerator FlashTimer()
    {
        if (timerText == null) yield break;
        
        timerText.color = warningColor;
        yield return new WaitForSeconds(penaltyFlashDuration);
        timerText.color = originalColor;
    }

    public float GetRemainingTime()
    {
        return currentTime;
    }
}