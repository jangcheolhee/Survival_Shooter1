using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class ZombieSpawner : MonoBehaviour
{
    
    public EnemyData[] zombieDatas;
    public Zombie[] zombie;
    private List<Zombie> zombies = new List<Zombie>();
    public GameManger manager;
   

    private int wave;

    private void Update()
    {
        if (zombies.Count == 0)
        {
            SpawnWave();
        }
    }
    
    private void SpawnWave()
    {
        wave++;
        int count = Mathf.RoundToInt(wave * 2.5f);
        for (int i = 0; i < count; i++)
        {
            CreateZombie();
        }
       
    }
    public void CreateZombie()
    {
        int r = Random.Range(0, zombieDatas.Length);
        NavMeshHit hit;

        Vector3 position = Random.insideUnitSphere * 10f;
        while (!NavMesh.SamplePosition(position, out hit, 10f, NavMesh.AllAreas))
        {
            position = Random.insideUnitSphere * 10f;
        }
        var zom = Instantiate(zombie[r]);
        zom.transform.position = hit.position;
        zom.Setup(zombieDatas[r]);
        zombies.Add(zom);

        zom.OnDeath += () => zombies.Remove(zom);
        zom.OnDeath += () => manager.AddScore(10);
        zom.OnDeath += () => Destroy(zom.gameObject, 5f);
    }

}
