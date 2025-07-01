using UnityEngine;

public class HintButton : MonoBehaviour
{
    [SerializeField] private TimerManager timerManager;
    
    public void OnHintButtonClicked()
    {
        if (timerManager != null)
        {
            timerManager.ApplyHintPenalty();
        }
        
        // Logika hint lainnya...
        Debug.Log("Hint digunakan! Waktu dikurangi");
    }
}