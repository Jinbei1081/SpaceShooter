using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    [System.Serializable]
    public class EnemyList
    {
        [SerializeField] public int weight;
        [SerializeField] public GameObject enemyPrefab;
    }


    public static GameObject WeightPick(List<EnemyList> enemyLists)
    {
        int totalWeight = 0;
        GameObject pick = default;

        //トータルの重み計算
        foreach (var weight in enemyLists)
        {
            totalWeight += weight.weight;
        }
        //抽選
        int rand = Random.Range(0, totalWeight);

        foreach (var i in enemyLists)
        {
            if (rand < i.weight)
            {
                pick = i.enemyPrefab;
                break;
            }
            rand -= i.weight;
        }

        return pick;
    }

}
