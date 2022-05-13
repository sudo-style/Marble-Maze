using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Marble : MonoBehaviour
{
    public GameObject go;
    public int score = 0;
    //start a timer and the begining of the game
    
    public float startTime = 0;

    void Start()
    {
        //start the timer
        startTime = Time.time;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        { 
            //Load the next scene
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            //print the time the player took to complete the maze
            print("Time: " + (Time.time - startTime));
        }
        //if they collide with the collectable then destroy the collectable and add one to the score
        if (other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            score++;
        }
    }

    //spawn new marble
    public void SpawnNewMarble(int height, int x, int y)
    {
        Instantiate(go, new Vector3(x, height, y), Quaternion.identity);
    }

    public void SpawnNewMarble()
    {
        Instantiate(go, new Vector3(0, 1, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //if the marbel goes out of bounds then it will be reset to the center of the maze
        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.z > 10 || transform.position.z < -10 || transform.position.y < -20)
        {
            SpawnNewMarble();
            //delete the marble
            Destroy(gameObject);

        }        
    }

    //display the time and score
    void OnGUI()
    {
        GUI.Label(new Rect(50, 10, 200, 40), "Time: " + (Time.time - startTime));
        GUI.Label(new Rect(50, 50, 200, 40), "Score: " + score);
    }
}
