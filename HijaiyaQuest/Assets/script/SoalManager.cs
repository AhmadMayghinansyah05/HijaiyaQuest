using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoalManager : MonoBehaviour
{
    public LevelData levelSaatIni;
    public TMP_Text teksSoal;
    public Button[] tombolJawaban;

    private int indexSoalSekarang = 0;

    void Start()
    {
        int levelTerbuka = PlayerPrefs.GetInt("LevelTerbuka", 1);
        if (levelSaatIni.levelIndex < levelTerbuka)
        {
            TampilkanSoal();
        }
        else
        {
            Debug.Log("Level belum terbuka atau sudah selesai sebelumnya.");
        }
    }

    void TampilkanSoal()
    {
        SoalData soalSekarang = levelSaatIni.daftarSoal[indexSoalSekarang];
        teksSoal.text = soalSekarang.soal;

        for (int i = 0; i < tombolJawaban.Length; i++)
        {
            int index = i;
            tombolJawaban[i].GetComponentInChildren<TMP_Text>().text = soalSekarang.jawaban[i];
            tombolJawaban[i].onClick.RemoveAllListeners();
            tombolJawaban[i].onClick.AddListener(() => PeriksaJawaban(index));
        }
    }

    void PeriksaJawaban(int indexPilihan)
    {
        SoalData soalSekarang = levelSaatIni.daftarSoal[indexSoalSekarang];
        if (indexPilihan == soalSekarang.indexJawabanBenar)
        {
            Debug.Log("Jawaban Benar!");
        }
        else
        {
            Debug.Log("Jawaban Salah!");
        }

        indexSoalSekarang++;
        if (indexSoalSekarang < levelSaatIni.daftarSoal.Length)
        {
            TampilkanSoal();
        }
        else
        {
            Debug.Log("Semua soal selesai. Level Selesai!");
            LevelProgressManager.BukaLevelBerikutnya(levelSaatIni.levelIndex);
        }
    }

    public void ResetSoal()
{
    indexSoalSekarang = 0;
    TampilkanSoal();
}

}