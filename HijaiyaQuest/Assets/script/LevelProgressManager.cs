using UnityEngine;
using UnityEngine.UI;

public class LevelProgressManager : MonoBehaviour
{
    public Button[] tombolLevel;
    private const string KEY_LEVEL = "LevelTerbuka";

    void Start()
    {
        int levelTerbuka = PlayerPrefs.GetInt(KEY_LEVEL, 1); // Default Level 1 terbuka

        for (int i = 0; i < tombolLevel.Length; i++)
        {
            tombolLevel[i].interactable = (i < levelTerbuka);
        }
    }

    public static void BukaLevelBerikutnya(int indexLevelSekarang)
    {
        int levelTerbuka = PlayerPrefs.GetInt(KEY_LEVEL, 1);

        if (indexLevelSekarang + 1 >= levelTerbuka)
        {
            PlayerPrefs.SetInt(KEY_LEVEL, indexLevelSekarang + 2); // +2 karena index 0 → Level 1
            PlayerPrefs.Save();
            Debug.Log("Level " + (indexLevelSekarang + 2) + " sekarang terbuka!");
        }
    }

    [ContextMenu("Reset Progress")]
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(KEY_LEVEL);
        Debug.Log("Progress di-reset");
    }
}