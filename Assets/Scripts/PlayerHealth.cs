using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private AudioSource audioSource;
    private Animator animator;
    public AudioClip hitClip;
    public AudioClip deathClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        audioSource.PlayOneShot(hitClip);
        base.OnDamage(damage, hitPosition, hitNormal);
    }
    protected override void Die()
    {
        audioSource.PlayOneShot(deathClip);
        animator.SetTrigger("Die");
        base.Die();
    }
    private void RestartLevel()
    {

    }

    
}
