using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFunction : MonoBehaviour
{
    [Header("Batas jatuh sebelum mati")]
    public float fallLimitY = -10f;

    void Update()
    {
        // Cek apakah posisi Y pemain di bawah batas jatuh
        if (transform.position.y < fallLimitY)
        {
            Die();
        }
    }

    // Fungsi untuk menangani kematian
    void Die()
    {
        Debug.Log("Player jatuh dan mati!");

        // Contoh: restart scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Anda bisa menambahkan efek mati lain di sini (animasi, suara, dsb)
        // Contoh:
        // GetComponent<Animator>().SetTrigger("Die");
        // audioSource.PlayOneShot(deathSound);
    }
}
