using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kotonoha : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    Transform akane = default, aoi = default;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            if (child.name == "ことのはあかね")
            {
                akane = child;
            }
            else
            {
                aoi = child;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aoi == null)
        {
            if (akane == null)
            {
                Destroy(gameObject);
                return;
            }
            akane.GetComponent<Akane>().Alone = true;
        }
    }
}
