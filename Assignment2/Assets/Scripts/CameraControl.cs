using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField][Range(20.0f, 60.0f)] private float speed = 40.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate (0, 0, Time.deltaTime*speed);
        } else if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate (0, 0, -Time.deltaTime*speed);
        }
        if(Input.GetKey(KeyCode.W)){
            transform.Rotate (Time.deltaTime*speed, 0, 0);
        } else if(Input.GetKey(KeyCode.S)){
            transform.Rotate (-Time.deltaTime*speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate (0, Time.deltaTime*speed, 0);
        } else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(0, -Time.deltaTime*speed, 0);
        }
    }
}
