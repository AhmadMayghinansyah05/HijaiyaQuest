using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Memastikan ada Collider2D
public class TrapTrigger : MonoBehaviour
{
    [Header("Trap Settings")]
    [Tooltip("Pastikan player memiliki tag 'Player'")]
    public string playerTag = "Player";
    public bool isOneTimeUse = true;
    public float killDelay = 0f;
    public string deathMessage = "KAMU MATI TERKENA JEBAKAN";

    [Header("Feedback")]
    public bool disableAfterTrigger = true;
    // public Animator trapAnimator;
    public string triggerAnimation = "Activate";
    public AudioClip trapSound;

    private bool isActivated = false;
    private AudioSource audioSource;
    private Collider2D trapCollider;

    void Start()
    {
        trapCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        
        Debug.Assert(trapCollider.isTrigger, "Collider harus di-set sebagai Trigger!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag) || (isOneTimeUse && isActivated)) 
            return;

        ActivateTrap(other.gameObject);
    }

    void ActivateTrap(GameObject player)
    {
        isActivated = true;

        // Feedback visual/audio
        // trapAnimator?.SetTrigger(triggerAnimation);
        if (trapSound != null) audioSource.PlayOneShot(trapSound);

        // Nonaktifkan collider jika perlu
        if (disableAfterTrigger) trapCollider.enabled = false;

        // Eksekusi kematian
        if (killDelay > 0)
            Invoke(nameof(KillPlayer), killDelay);
        else
            KillPlayer();

        void KillPlayer()
        {
            if (player.TryGetComponent(out PlayerFunction playerFunction))
                playerFunction.Die(deathMessage);
            else
                Debug.LogError($"Player tidak memiliki {nameof(PlayerFunction)}!", player);
        }
    }

    public void ResetTrap()
    {
        isActivated = false;
        trapCollider.enabled = true;
    }
}