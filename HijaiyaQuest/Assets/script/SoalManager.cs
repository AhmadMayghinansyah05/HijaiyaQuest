using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoalManager : MonoBehaviour
{
    public TMP_Text teksSoal;
    public Button[] tombolJawaban;
    public GameObject panelSoal;
    public Button hintButton; // Tambahkan reference ke tombol hint

    private SoalTrigger triggerAktif;
    private SoalData soalAktif;

    void Start()
    {
        panelSoal.SetActive(false);
        
        // Setup listener untuk tombol hint
        if (hintButton != null)
        {
            hintButton.onClick.AddListener(OnHintUsed);
        }
        else
        {
            Debug.LogWarning("Tombol hint belum di-assign di SoalManager");
        }
    }

    public void TampilkanSoal(SoalData soal, SoalTrigger trigger)
    {
        if (teksSoal == null || tombolJawaban == null || panelSoal == null)
        {
            Debug.LogError("Komponen UI belum di-assign di SoalManager!");
            return;
        }

        triggerAktif = trigger;
        soalAktif = soal; // Simpan soal aktif
        teksSoal.text = soal.soal;

        for (int i = 0; i < tombolJawaban.Length; i++)
        {
            if (tombolJawaban[i] == null)
            {
                Debug.LogError($"Tombol jawaban {i} belum di-assign!");
                continue;
            }

            int index = i;
            tombolJawaban[i].GetComponentInChildren<TMP_Text>().text = soal.jawaban[i];
            tombolJawaban[i].onClick.RemoveAllListeners();
            tombolJawaban[i].onClick.AddListener(() => PeriksaJawaban(index, soal));
        }

        panelSoal.SetActive(true);
    }

    void PeriksaJawaban(int indexPilihan, SoalData soal)
    {
        if (indexPilihan == soal.indexJawabanBenar)
        {
            Debug.Log("Jawaban Benar!");
            triggerAktif.TandaiSudahDijawab();
        }
        else
        {
            Debug.Log("Jawaban Salah!");
            triggerAktif.OnWrongAnswer(); // Panggil penalty waktu
        }

        panelSoal.SetActive(false);
    }

    // Method untuk tombol hint
    public void OnHintUsed()
    {
        if (triggerAktif != null)
        {
            triggerAktif.OnHintUsed(); // Panggil penalty waktu
            
            // Tambahkan logika hint di sini
            Debug.Log($"Hint digunakan! Jawaban benar adalah: {soalAktif.jawaban[soalAktif.indexJawabanBenar]}");
            
            // Contoh: tampilkan jawaban benar (bisa disesuaikan)
            teksSoal.text += $"\n\n<color=yellow>Hint: Jawaban dimulai dengan '{soalAktif.jawaban[soalAktif.indexJawabanBenar][0]}'</color>";
        }
    }
}