using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private AudioSource audioSource;
    public ParticleSystem particle;
    public AudioClip shotClip;
    public float damage = 25f;
    public float timeBetFire = 0.2f;
    public float fireDistace = 50f;
    private float lastFireTime;

    public Transform firePosition;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }
    public void Fire()
    {
        if (Time.time > lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shoot();
        }
    } 
    public void Shoot()
    {
        Vector3 hitPosition = Vector3.zero;
        RaycastHit hit;
        if(Physics.Raycast(firePosition.position, firePosition.forward,out hit,fireDistace))
        {
            hitPosition = hit.point;
            var target = hit.collider.GetComponent<IDamgable>();
            if (target != null)
            {
                target.OnDamage(damage, hitPosition, hit.normal);
            }

        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * fireDistace;
        }
        StartCoroutine(ShotEffect(hitPosition));
    }
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        particle.transform.position = firePosition.position;
        particle.Play();
        audioSource.PlayOneShot(shotClip);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPosition);
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}
