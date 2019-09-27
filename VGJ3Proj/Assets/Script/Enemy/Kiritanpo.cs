using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiritanpo : MonoBehaviour
{
    public GameObject knife;
    [SerializeField] float span;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(11, -4.8f, 0);
        InvokeRepeating("ShotKnife", 1f, span);
    }

    private void Update()
    {
        if (GetComponent<Enemy>().hp <= 0)
        {
            CancelInvoke("ShotKnife");
        }
    }

    void ShotKnife()
    {
        Vector3 pos = new Vector3(transform.position.x-0.743f, transform.position.y+0.723f, 0);
        Instantiate(knife, pos, Quaternion.Euler(new Vector3(0, 0, -15)));
    }
}
