using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = Score._Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene("Game"); }
        if (Input.GetKeyDown(KeyCode.T)) { SceneManager.LoadScene("Title"); }

    }
}
