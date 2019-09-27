using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.x -= speed;
        transform.position = pos;

        if (transform.position.x < -19.5)
        {
            transform.Translate(19.5f*2, 0, 0);

        }
    }
}
