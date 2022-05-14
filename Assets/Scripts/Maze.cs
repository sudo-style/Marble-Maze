using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

        //if the current cell has any neighbors which have not been visited
        List<Cell> neighbors = currentCell.getNeighbors(this);
        /*List<Cell> unvisitedNeighbors = new List<Cell>();
        foreach(Cell cell in neighbors){
            if(cell.visited == false){
                unvisitedNeighbors.Add(cell);
            }
        }*/
        






        //Creates all of the walls according to the grid
        foreach(Cell cell in grid){
            cell.makeWall();
        }
    }
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

    public List<Cell> getNeighbors(Maze maze){
        List<Cell> neighbors = new List<Cell>();

        int currentPos = maze.currentCell.y * maze.rows + maze.currentCell.x;
        int topIndex = currentPos - maze.rows;
        int rightIndex = currentPos + 1;
        int bottomIndex = currentPos + maze.rows;
        int leftIndex = currentPos - 1;

        if (currentPos >= maze.cols){
            //not on top
            Cell top = maze.grid[topIndex];
            if (top.visited == false){
                neighbors.Add(top);
            }
        }
        if (currentPos % maze.cols != maze.cols - 1){
            //not on right
            Cell right = maze.grid[rightIndex];
            if (right.visited == false){
                neighbors.Add(right);
            }
        }
        if (currentPos < maze.rows * maze.cols - maze.cols){
            //not on bottom
            Cell bottom = maze.grid[bottomIndex];
            if (bottom.visited == false){
                neighbors.Add(bottom);
            }
        }
        if (currentPos % maze.rows != 0){
            //not on left
            Cell left = maze.grid[leftIndex];
            if (left.visited == false){
                neighbors.Add(left);
            }
        }
        return neighbors;
    }
}
}   


