using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Cell{
    public int x;
    public int y;
    public bool north = true;
    public bool south = true;
    public bool east = true;
    public bool west = true;

    public Cell(int x, int y){
        this.x = x;
        this.y = y; 
    }

}

public class Maze : MonoBehaviour{
    private int col = 10;
    private int row = 10;
    private List<Cell> grid = new List<Cell>();

    public void Start(){
        for (int i= 0; i<row; i++){
            for (int j= 0; j<col; j++){
                Cell cell = new Cell(i,j);
                grid.Add(cell);
            }
        }

        //create all cells

        //create the maze
        foreach(Cell cell in grid){
            createCellWall(cell);
        }
    }




    //depending on the status of the cell, create a wall
    public void createCellWall(Cell cell){
        if (cell.north){
            GameObject wallNorth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallNorth.transform.position = new Vector3(cell.x, 0, cell.y+0.5f);
            wallNorth.transform.localScale = new Vector3(1,1,0.1f);
            wallNorth.GetComponent<Renderer>().material.color = Color.black;
        } 
        if (cell.south){
            GameObject wallSouth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallSouth.transform.position = new Vector3(cell.x, 0, cell.y-0.5f);
            wallSouth.transform.localScale = new Vector3(1,1,0.1f);
            wallSouth.GetComponent<Renderer>().material.color = Color.black;
        }
        if (cell.east){
            GameObject wallSouth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallSouth.transform.position = new Vector3(cell.x+0.5f, 0, cell.y);
            wallSouth.transform.localScale = new Vector3(0.1f,1,1);
            wallSouth.GetComponent<Renderer>().material.color = Color.black;
        }
        if (cell.west){
            GameObject wallSouth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallSouth.transform.position = new Vector3(cell.x-0.5f, 0, cell.y);
            wallSouth.transform.localScale = new Vector3(0.1f,1,1);
            wallSouth.GetComponent<Renderer>().material.color = Color.black;
        }
    }
} 