using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoalManager : MonoBehaviour
{
    public TMP_Text teksSoal;
    public Button[] tombolJawaban;
    public GameObject panelSoal;

    private SoalTrigger triggerAktif;

    void Start()
    {
        panelSoal.SetActive(false);
    }

    public void TampilkanSoal(SoalData soal, SoalTrigger trigger)
{
    if (teksSoal == null || tombolJawaban == null || panelSoal == null)
    {
        Debug.LogError("Komponen UI belum di-assign di SoalManager!");
        return;
    }

    triggerAktif = trigger;
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
        }

        panelSoal.SetActive(false);
    }
}
