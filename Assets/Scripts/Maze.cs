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


        bool currentCellHasUnvisitedNeighbors = true;
        while(currentCellHasUnvisitedNeighbors){
            //if the current cell has any neighbors which have not been visited
            List<Cell> neighbors = currentCell.getNeighbors(this);
            List<Cell> unvisitedNeighbors = new List<Cell>();
            foreach(Cell cell in neighbors){
                if(cell.visited == false){
                    unvisitedNeighbors.Add(cell);
                }
            }
            currentCellHasUnvisitedNeighbors = unvisitedNeighbors.Count != 0;

            //Pick a random neighbor from the list of unvisited neighbors, this will be the next cell
            if (currentCellHasUnvisitedNeighbors){
                int randomNeighborIndex = Random.Range(0,unvisitedNeighbors.Count);
                Cell nextCell = unvisitedNeighbors[randomNeighborIndex];
                removeWall(currentCell,nextCell);
                if (unvisitedNeighbors!= null){
                    nextCell.visited = true;
                    currentCell = nextCell;
                }
            }
        }

        //Creates all of the walls according to the grid
        foreach(Cell cell in grid){
            cell.makeWall();
        }
    }

     public void removeWall(Cell current, Cell next){
        //current is in the middle
        //next is either top, right, bottom or left
        Debug.Log("Next: " + next.x + "," + next.y + "\n");
        
        if (current.x == next.x){
            //either top or bottom
            if (current.y > next.y){
                //top
                current.walls[0] = false;
                next.walls[2] = false;
            }else{
                //bottom
                current.walls[2] = false;
                next.walls[0] = false;
            }
        }
        else if (current.y == next.y){
            //either left or right
            if (current.x > next.x){
                //left
                current.walls[3] = false;
                next.walls[1] = false;
            }else{
                //right
                current.walls[1] = false;
                next.walls[3] = false;
            }
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
        if(walls[2]){
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
        if(walls[0]){
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
            Cell top = maze.grid[topIndex];
            neighbors.Add(top);
        }
        if (currentPos % maze.cols != maze.cols - 1){
            Cell right = maze.grid[rightIndex];
            neighbors.Add(right);
        }
        if (currentPos < maze.rows * maze.cols - maze.cols){
            Cell bottom = maze.grid[bottomIndex];
            neighbors.Add(bottom);
        }
        if (currentPos % maze.rows != 0){
            Cell left = maze.grid[leftIndex];
            neighbors.Add(left);
        }
        return neighbors;
    }
}
}   


