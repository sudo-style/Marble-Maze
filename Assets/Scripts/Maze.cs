using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell{
    public int x;
    public int y;
    public bool[] walls = {true,true,true,true};
    public bool visited = false;

    public Cell(int x, int y){
        this.x = x;
        this.y = y; 
    }   
    public void makeWall(){
        if(walls[0]){
            //top
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.localPosition = new Vector3(x,0.5f,y+0.5f);
            wall.transform.localScale = new Vector3(1,1,0.1f); 
        }
        if(walls[1]){
            //right
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.localPosition = new Vector3(x+0.5f,0.5f,y);
            wall.transform.localScale = new Vector3(0.1f,1,1); 
        }
        if(walls[2]){
            //bottom
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.localPosition = new Vector3(x,0.5f,y-0.5f);
            wall.transform.localScale = new Vector3(1,1,0.1f);

        }
        if(walls[3]){
            //left
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.localPosition = new Vector3(x-0.5f,0.5f,y);
            wall.transform.localScale = new Vector3(0.1f,1,1); 
        }
    }
}

public class Maze : MonoBehaviour{  
    public int cols = 10, rows = 10;
    public int cellWidth = 1, cellHeight = 1;
    private List<Cell> grid = new List<Cell>();
    public GameObject wall; 
    public Cell currentCell;


    public void Start(){
        //Sets up the grid
        for(int posX = 0; posX<rows; posX++){
            for(int posY = 0; posY<cols; posY++){
                Cell cell = new Cell(posY,posX);
                grid.Add(cell);
            }
        }

        //Pick a point on the grid
        currentCell = grid[0];
        //Mark the current cell as visited
        currentCell.visited = true;




        //Creates all of the walls according to the grid
        foreach(Cell cell in grid){
            cell.makeWall();
        }
    }
}   


