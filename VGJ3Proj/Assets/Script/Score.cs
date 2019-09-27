using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{ 
    public static int _Score;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        _Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = _Score.ToString();
    }

    public void AddScore(int _score)
    {
        _Score += _score;
    }
}
