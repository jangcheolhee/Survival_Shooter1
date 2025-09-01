using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : LivingEntity
{
    public enum Status
    {
        Idle,
        Trace,
        Attack,
        Die,
    }
    private Status currentStatus;
    public Status CurrentStatus
    {
        get { return currentStatus; }
        set
        {
            var prevStatus = CurrentStatus;
            currentStatus = value;
            switch (currentStatus)
            {
                case Status.Idle:
                    animator.SetBool("Trace", false);
                    agent.isStopped = true;
                    break;
                case Status.Trace:
                    animator.SetBool("Trace", true);
                    agent.isStopped = false;
                    break;
                case Status.Attack:
                    animator.SetBool("Trace", false);
                    agent.isStopped = true;
                    break;
                case Status.Die:
                    animator.SetTrigger("Die");
                    agent.isStopped = false;
                    collider.enabled = false;
                    break;
            }
        }
    }
    private Collider collider;
    public float traceDistance;
    public float attackDistance;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    private Animator animator;
    private AudioClip hitClip;
    private AudioClip deathClip;
    public ParticleSystem particle;
    private float lastAttackTime;
    public float attackInterval;
    private float damage;
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        CurrentStatus = Status.Idle;
        collider.enabled = true;
    }
    public void Setup(EnemyData data)
    {
        MaxHp = data.maxHP;
        damage = data.damage;
        agent.speed= data.speed;
        hitClip = data.hitClip;
        deathClip = data.deathClip;

    }
    private void Update()
    {
        switch (currentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();
                break;
        }

    }

    private void UpdateIdle()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) < traceDistance)
        {
            CurrentStatus = Status.Trace;
        }
        target = FindTarget(traceDistance);
    }

    private void UpdateDie()
    {
        
    }

    private void UpdateAttack()
    {
        if (target == null || target != null && Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            CurrentStatus = Status.Trace;
            return;
        }
        var lookAt = target.position;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);

        if (lastAttackTime + attackInterval < Time.time)
        {
            lastAttackTime = Time.time;
            var damagalble = target.GetComponent<IDamgable>();
            if (damagalble != null)
            {
                damagalble.OnDamage(damage, transform.position, -transform.forward);
            }
        }
    }

    private void UpdateTrace()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) < attackDistance)
        {
            CurrentStatus = Status.Attack;
            return;
        }
        if (target == null || Vector3.Distance(transform.position, target.position) > traceDistance)
        {
            CurrentStatus = Status.Idle;
            return;
        }
        agent.SetDestination(target.position);
    }

    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPosition, hitNormal);
        audioSource.PlayOneShot(hitClip);
        particle.transform.position = hitPosition;
        particle.transform.forward = hitNormal;
        particle.Play();
    }

    protected override void Die()
    {
        base.Die();
        audioSource.PlayOneShot(deathClip);
        CurrentStatus = Status.Die;
    }

    public LayerMask targetLayer;
    private Transform FindTarget(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        if(colliders.Length == 0)
        {
            return null;
        }
        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }
}
