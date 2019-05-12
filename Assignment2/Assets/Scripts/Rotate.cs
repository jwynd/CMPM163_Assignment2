using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField][Range(0.0f, 100.0f)] private float speed;
    private RenderEffectBloom reb;
    // Start is called before the first frame update
    void Start()
    {
        reb = GameObject.Find("Main Camera").GetComponent<RenderEffectBloom>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        transform.Rotate(0, Time.deltaTime*speed, 0);
        else if(Input.GetKey(KeyCode.D))
        transform.Rotate(0, -Time.deltaTime*speed, 0);
        if(Input.GetKey(KeyCode.W))
        transform.Rotate(Time.deltaTime*speed, 0, 0);
        else if(Input.GetKey(KeyCode.S))
        transform.Rotate(-Time.deltaTime*speed, 0, 0);

        if(Input.GetKey(KeyCode.LeftArrow) && reb.BloomFactor > 0.1f) reb.BloomFactor -= Time.deltaTime*10;
        else if(Input.GetKey(KeyCode.RightArrow) && reb.BloomFactor < 100.0f) reb.BloomFactor += Time.deltaTime*10;
    }
}
