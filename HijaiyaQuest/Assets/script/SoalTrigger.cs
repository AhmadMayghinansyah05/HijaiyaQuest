using UnityEngine;

public class SoalTrigger : MonoBehaviour
{
    public GameObject panelSoal;
    public SoalData dataSoal; // assign ScriptableObject soal
    private SoalManager soalManager;
    private bool sudahDijawab = false;

void Start()
{
    // Cek SoalManager
    soalManager = FindObjectOfType<SoalManager>();
    if (soalManager == null)
    {
        Debug.LogError("SoalManager tidak ditemukan! Pastikan ada di scene.");
    }

    // Cek PanelSoal
    if (panelSoal == null)
    {
        Debug.LogError("panelSoal belum di-assign! Drag Panel UI ke Inspector.");
        enabled = false; // Nonaktifkan script
    }
    else
    {
        panelSoal.SetActive(false);
    }
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sudahDijawab)
        {
            soalManager.TampilkanSoal(dataSoal, this); // ⬅️ panggil di sini!
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sudahDijawab)
        {
            if (panelSoal != null)
                panelSoal.SetActive(false);
        }
    }

    public void TandaiSudahDijawab()
    {
        sudahDijawab = true;
        GetComponent<Collider2D>().enabled = false;
    }

    public void ResetTrigger()
    {
        sudahDijawab = false;
        GetComponent<Collider2D>().enabled = true;
    }
}
