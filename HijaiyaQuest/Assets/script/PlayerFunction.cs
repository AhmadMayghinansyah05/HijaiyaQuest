using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; // Diperlukan untuk Button

public class PlayerFunction : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallLimitY = -10f;
    
    [Header("Death Settings")]
    public float deathDelay = 1f;
    public AudioClip deathSound;
    public string deathAnimationTrigger = "Die";
    
    [Header("UI Settings")]
    public GameObject deathScreenUI;
    public TextMeshProUGUI deathMessageText;
    public Button restartButton; // Tombol restart
    public Button mainMenuButton; // Tombol ke main menu

    [Header("Player Settings")]
    public string playerTag = "Player";
    public string mainMenuSceneName = "MainMenu"; // Nama scene main menu
    
    // private Animator animator;
    private AudioSource audioSource;
    private bool isDead = false;
    
    void Start()
    {
        // Get components
        // animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameObject.tag = playerTag;
        
        // Setup UI awal
        if (deathScreenUI != null)
            deathScreenUI.SetActive(false);
            
        // Setup tombol
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);
            
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }
    
    void Update()
    {
        if (!isDead && transform.position.y < fallLimitY)
        {
            Die("KAMU TERJATUH DAN MATI");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag("Trap"))
        {
            Die("KAMU MATI TERKENA JEBAKAN");
        }
    }
    
    public void Die(string deathMessage)
    {
        if (isDead) return;
        isDead = true;
        
        // Matikan semua kontrol dan physics
        GetComponent<PlayerMovement>().enabled = false;
        
         // Handle kedua tipe Rigidbody
    var rb2D = GetComponent<Rigidbody2D>();
    if (rb2D != null)
    {
        rb2D.velocity = Vector2.zero;
        rb2D.simulated = false;
    }
    
    var rb3D = GetComponent<Rigidbody>();
    if (rb3D != null)
    {
        rb3D.velocity = Vector3.zero;
        rb3D.isKinematic = true;
    }
    
    // Nonaktifkan semua collider
    foreach (var col in GetComponents<Collider>()) col.enabled = false;
    foreach (var col in GetComponents<Collider2D>()) col.enabled = false;
        
        // Animasi dan suara
        // if (animator != null)
        //     animator.SetTrigger(deathAnimationTrigger);
            
        if (audioSource != null && deathSound != null)
            audioSource.PlayOneShot(deathSound);
        
        // Tampilkan UI kematian
        ShowDeathScreen(deathMessage);
        
        // Batalkan restart otomatis jika ada tombol manual
        CancelInvoke("RestartLevel");
    }
    
    void ShowDeathScreen(string message)
    {
        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(true);
            
            if (deathMessageText != null)
                deathMessageText.text = message;
        }
    }
    
    public void RestartLevel()
    {
        // Reload scene aktif
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void GoToMainMenu()
    {
        // Load scene main menu
        SceneManager.LoadScene(mainMenuSceneName);
    }
}