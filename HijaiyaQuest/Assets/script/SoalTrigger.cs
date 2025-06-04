using UnityEngine;

public class SoalTrigger : MonoBehaviour
{
    public GameObject panelSoal; // Drag panel popup dari Canvas ke sini

    void Start()
    {
        if (panelSoal != null)
            panelSoal.SetActive(false); // pastikan awalnya tidak tampil
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelSoal != null)
                panelSoal.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelSoal != null)
                panelSoal.SetActive(false);
        }
    }
}
