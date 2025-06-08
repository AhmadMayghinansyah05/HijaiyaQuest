using UnityEngine;

public class ProgressResetter : MonoBehaviour
{
    public SoalManager soalManager; // referensi ke SoalManager agar bisa reset soal juga

    public void ResetProgress()
    {
        // Reset data level terbuka, set ulang ke level 1
        PlayerPrefs.SetInt("LevelTerbuka", 1);
        PlayerPrefs.Save();

        // Reset soal yang sedang dikerjakan ke awal
        if (soalManager != null)
        {
            soalManager.ResetSoal();
        }

        Debug.Log("Progress berhasil direset!");
    }
}
