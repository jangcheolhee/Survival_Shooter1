using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public AudioClip hitClip;
    public AudioClip deathClip;
    public float maxHP = 100;
    public float damage = 20;
    public float speed = 2f;
}
