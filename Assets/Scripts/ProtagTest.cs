using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagTest : MonoBehaviour
{
    [Range(0.001f, 0.02f)]
    public float slowFactor;
    public string turnDir = "right";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * slowFactor, Input.GetAxis("Vertical") * slowFactor, 0);

        if(Input.GetAxis("Horizontal") > 0){
            gameObject.transform.localScale = new Vector3(0.5f,0.5f,1);
            turnDir = "right";
        }
        else if (Input.GetAxis("Horizontal") < 0){
            gameObject.transform.localScale = new Vector3(-0.5f,0.5f,1);
            turnDir = "left";
        }
    }
}
