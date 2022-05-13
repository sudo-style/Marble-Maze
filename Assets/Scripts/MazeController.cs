using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    // Start is called before the first frame update

    private float rotationX = 0f;   
    private float rotationY = 0f;
    private float rotationAngleX = 0f;
    private float rotationAngleY = 0f;

    void GetInput(){
        rotationX = Input.GetAxis("Horizontal");
        rotationY = Input.GetAxis("Vertical");
    }

   // Update is called once per frame
    void Update()
    {
        GetInput();
        //Tilt the maze depending on the user input smoothy
    
        transform.Rotate(new Vector3(0, 0, rotationX * Time.deltaTime * -100));
        transform.Rotate(new Vector3(rotationY * Time.deltaTime * 100, 0, 0));

        //Find the angle of the maze
        rotationAngleX = transform.rotation.eulerAngles.x;
        rotationAngleY = transform.rotation.eulerAngles.z;

        //If the user isn't inputing then the maze will go back to the original position smoothly
        if (rotationX == 0 && rotationY == 0){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 10);
        }

    }

     void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 300, 200, 40), "X: " + rotationAngleX );
        GUI.Label(new Rect(500, 350, 200, 40), " Y: " + rotationAngleY);
    }
}
