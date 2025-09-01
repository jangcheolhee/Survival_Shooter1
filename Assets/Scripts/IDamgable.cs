using UnityEngine;

public interface IDamgable
{
    public void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal);
}
