using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressResetter : MonoBehaviour
{
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); // Reset semua data progres
        Debug.Log("Progress telah di-reset!");

        // Reload scene untuk reset semua trigger
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
