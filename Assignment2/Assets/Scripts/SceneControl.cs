using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private string[] scenes = {"Part1","Part2"};
    private string thisScene;
    private int otherScene;
    // Start is called before the first frame update
    void Start()
    {
        thisScene = SceneManager.GetActiveScene().name;
        otherScene = thisScene == "Part1"?1:0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene(scenes[otherScene], LoadSceneMode.Single);
        }
    }
}
