using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    SpriteRenderer renderer;
    float trans = 1;
    float scale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trans <= 0) { Destroy(gameObject); return; }

        scale += 0.03f;
        transform.localScale=new Vector3(scale, scale, 1);
        trans -= 0.03f;
        renderer.color = new Color(1f, 1f, 1f, trans);
    }
}
