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
}

public class Maze : MonoBehaviour{
    private int col = 10;
    private int row = 10;
    private List<Cell> grid = new List<Cell>();

    public void removeWall(Cell current, Cell next){
        //current is in the middle
        //next is either top, right, bottom or left
        
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
        else{
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

    public List<Cell> getUnvisitedNeighbor(Cell current){
            List<Cell> neighbors = new List<Cell>();
            int top = current.y + 1;
            int bottom = current.y - 1;
            int left = current.x - 1;
            int right = current.x + 1;

            if(top < col){
                if(!grid[top*col + current.x].visited){
                    neighbors.Add(grid[top*col + current.x]);
                }
            }
            if(bottom >= 0){
                if(!grid[bottom*col + current.x].visited){
                    neighbors.Add(grid[bottom*col + current.x]);
                }
            }
            if(left >= 0){
                if(!grid[current.y*col + left].visited){
                    neighbors.Add(grid[current.y*col + left]);
                }
            }
            if(right < row){
                if(!grid[current.y*col + right].visited){
                    neighbors.Add(grid[current.y*col + right]);
                }
            }
            return neighbors;
        }

    public bool hasUnvisitedNeighbors(Cell current){
            int top = current.y + 1;
            int bottom = current.y - 1;
            int left = current.x - 1;
            int right = current.x + 1;

            if(top < col){
                if(!grid[top*col + current.x].visited){
                    return true;
                }
            }
            if(bottom >= 0){
                if(!grid[bottom*col + current.x].visited){
                    return true;
                }
            }
            if(left >= 0){
                if(!grid[current.y*col + left].visited){
                    return true;
                }
            }
            if(right < row){
                if(!grid[current.y*col + right].visited){
                    return true;
                }
            }
            return false;
        }
    
    public void Start(){
        for (int i= 0; i<row; i++){
            for (int j= 0; j<col; j++){
                Cell cell = new Cell(i,j);
                grid.Add(cell);
            }
        }

        //given the current cell as a paramater
        Cell current = grid[0];

        //Mark the current cell as visited
        current.visited = true;

        //While the current cell has any unvisited neighbors
        while(hasUnvisitedNeighbors(current)){
            //find the neighbors of the current cell
            List<Cell> neighbors = getUnvisitedNeighbor(current);
            //randomly select one of the neighbors
            Cell next = neighbors[Random.Range(0, neighbors.Count)];
            //remove the wall between the current cell and the selected neighbor
            removeWall(current, next);
            //mark the selected neighbor as visited
            next.visited = true;
            //make the selected neighbor the current cell
            current = next;
        }

        //create the maze
        foreach(Cell cell in grid){
            createCellWall(cell);
        }
    }

    //depending on the status of the cell, create a wall
    public void createCellWall(Cell cell){
        if (cell.walls[0]){
            GameObject wallNorth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallNorth.transform.position = new Vector3(cell.x, 0, cell.y+0.5f);
            wallNorth.transform.localScale = new Vector3(1,1,0.1f);
            wallNorth.GetComponent<Renderer>().material.color = Color.black;
        } 
        if (cell.walls[1]){
            GameObject wallSouth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallSouth.transform.position = new Vector3(cell.x+0.5f, 0, cell.y);
            wallSouth.transform.localScale = new Vector3(0.1f,1,1);
            wallSouth.GetComponent<Renderer>().material.color = Color.black;
        }
        if (cell.walls[2]){
            GameObject wallSouth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallSouth.transform.position = new Vector3(cell.x, 0, cell.y-0.5f);
            wallSouth.transform.localScale = new Vector3(1,1,0.1f);
            wallSouth.GetComponent<Renderer>().material.color = Color.black;
        }
        if (cell.walls[3]){
            GameObject wallSouth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wallSouth.transform.position = new Vector3(cell.x-0.5f, 0, cell.y);
            wallSouth.transform.localScale = new Vector3(0.1f,1,1);
            wallSouth.GetComponent<Renderer>().material.color = Color.black;
        }
    }
} 