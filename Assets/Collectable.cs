using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float the collectable up and down
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * 0.5f + 1, transform.position.z);

        //have the collectable rotate around the maze 
        float rotationAngle = Time.deltaTime * 100;
        transform.Rotate(new Vector3(rotationAngle,rotationAngle*0.5f,rotationAngle*1.3f));
    }
}
