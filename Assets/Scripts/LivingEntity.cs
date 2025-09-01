using UnityEngine;
using System;
public class LivingEntity : MonoBehaviour, IDamgable
{
    public bool IsDead {  get; private set; }
    public float MaxHp { get; set; } = 100f;
    public float Hp {  get; private set; }

    public event Action OnDeath;

    protected virtual void OnEnable()
    {
        Hp = MaxHp;
        IsDead = false;
    }
    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        Hp -= damage;
        if(Hp <= 0 && !IsDead)
        {
            Die();
        }
    }
    protected virtual void Die() 
    {
        OnDeath?.Invoke();
        IsDead = true;
    }

}
