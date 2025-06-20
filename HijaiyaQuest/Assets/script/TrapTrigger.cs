using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [Header("Trap Settings")]
    public string playerTag = "Player"; // Tag player
    public int damageAmount = 1; // Jumlah damage
    public bool instantKill = true; // Apakah langsung membunuh?
    
    [Header("Effects")]
    public AudioClip triggerSound;
    public GameObject triggerEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        // Cek jika yang menyentuh adalah player
        if (other.CompareTag(playerTag))
        {
            TriggerTrap(other.gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Backup detection jika trigger tidak bekerja
        if (collision.gameObject.CompareTag(playerTag))
        {
            TriggerTrap(collision.gameObject);
        }
    }
    
    void TriggerTrap(GameObject player)
    {
        // Mainkan efek suara
        if (triggerSound != null)
        {
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
        }
        
        // Instantiate efek visual
        if (triggerEffect != null)
        {
            Instantiate(triggerEffect, transform.position, transform.rotation);
        }
        
        // Dapatkan komponen PlayerFunction dari player
        PlayerFunction playerFunction = player.GetComponent<PlayerFunction>();
        if (playerFunction != null)
        {
            if (instantKill)
            {
                playerFunction.Die("KAMU MATI TERKENA JEBAKAN");
            }
            else
            {
                // Implementasi damage system jika perlu
                // playerFunction.TakeDamage(damageAmount);
            }
        }
        
        // Nonaktifkan trap setelah triggered (opsional)
        // gameObject.SetActive(false);
    }
}