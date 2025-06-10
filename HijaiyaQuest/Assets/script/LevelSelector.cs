using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public LevelData[] semuaLevel;
    public Button[] tombolLevel;

    void Start()
    {
        int levelTerbuka = PlayerPrefs.GetInt("LevelTerbuka", 1); // Default level 1 terbuka

        for (int i = 0; i < tombolLevel.Length; i++)
        {
            int index = i;

            // Cek apakah level ini sudah terbuka
            bool terbuka = semuaLevel[i].levelIndex < levelTerbuka;

            tombolLevel[i].interactable = terbuka;
            tombolLevel[i].onClick.RemoveAllListeners();

            if (terbuka)
            {
                tombolLevel[i].onClick.AddListener(() => BukaLevel(index));
            }
            else
            {
                tombolLevel[i].onClick.AddListener(() => Debug.Log("Level belum terbuka!"));
            }
        }
    }

    void BukaLevel(int index)
    {
        Debug.Log("Buka level ke-" + index);
        // Tambahkan logika untuk load scene atau tampilkan level
    }
}