using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Gantikan UnityEngine.UI

public class PlayerFunction : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallLimitY = -10f;
    
    [Header("Death Settings")]
    public float deathDelay = 1f; // Waktu delay sebelum restart
    public AudioClip deathSound; // Suara saat mati
    public string deathAnimationTrigger = "Die"; // Nama trigger animasi
    
    [Header("UI Settings")]
    public GameObject deathScreenUI; // Panel UI yang muncul saat mati
    public TextMeshProUGUI deathMessageText;

    [Header("Player Settings")]
    public string playerTag = "Player";
    
    private Animator animator;
    private AudioSource audioSource;
    private bool isDead = false;
    
    void Start()
    {
        // Get components
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameObject.tag = playerTag;
        
        // Pastikan UI mati tidak aktif di awal
        if (deathScreenUI != null)
            deathScreenUI.SetActive(false);
    }
    
    void Update()
    {
        // Cek jatuh
        if (!isDead && transform.position.y < fallLimitY)
        {
            Die("KAMU TERJATUH DAN MATI");
        }
    }
    
    // Dipanggil ketika player menyentuh jebakan
    void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag("Trap"))
        {
            Die("KAMU MATI TERKENA JEBAKAN");
        }
    }
    
    // Fungsi untuk menangani semua jenis kematian
    public void Die(string deathMessage)
    {
        isDead = true;
        Debug.Log("Player died: " + deathMessage);
        
        // Matikan kontrol player
        GetComponent<PlayerMovement>().enabled = false;
        
        // Trigger animasi mati jika ada
        if (animator != null)
        {
            animator.SetTrigger(deathAnimationTrigger);
        }
        
        // Mainkan suara mati jika ada
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        
        // Tampilkan UI kematian
        ShowDeathScreen(deathMessage);
        
        // Restart level setelah delay
        Invoke("RestartLevel", deathDelay);
    }
    
    void ShowDeathScreen(string message)
    {
        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(true);
            
            // Set pesan kematian
            if (deathMessageText != null)
            {
                deathMessageText.text = message;
            }
        }
    }
    
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}