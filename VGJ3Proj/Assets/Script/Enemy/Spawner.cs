using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{  

    [SerializeField] List<Utils.EnemyList> lists = new List<Utils.EnemyList>();
    [SerializeField] float spawnSpan = 0.5f;

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnSpan);
    }

    void Spawn()
    {
        var pos = new Vector3(11, Random.Range(-4.5f, 3.5f), 0);
        Instantiate(Utils.WeightPick(lists), pos, Quaternion.identity);
    }

    public void Stop()
    {
        CancelInvoke("Spawn");
    }

    
}
