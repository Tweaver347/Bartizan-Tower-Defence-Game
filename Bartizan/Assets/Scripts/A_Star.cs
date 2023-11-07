using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using gridCell;

public class A_Star
{
    public static List<Vector3> findPath(Grid_Cell[,] grid, Grid_Cell start, Grid_Cell end)
    {
        Debug.Log("In find path");
        List<Vector3> path = new List<Vector3>();

        List<Grid_Cell> frontier = new List<Grid_Cell>();
        ArrayList visited = new ArrayList();

        frontier.Add(start);

        Grid_Cell startPoint = start;
        startPoint.SetDistTo(0);

        Debug.Log("starting while loop");
        while(frontier.Count > 0)
        {
            Grid_Cell current = getClosestCell(frontier); //get the cell with lowest cost
            frontier.Remove(current);
            visited.Add(current);
            Grid_Cell[] neighbours = current.GetNeighbours();
            Debug.Log("Current cell: " + current.GetNumber());
            Debug.Log("checking neighbours");
            for (int i = 0; i < neighbours.Length; i++)
            {
                Grid_Cell nextCell = neighbours[i];
                Debug.Log("Neighbour found: " + nextCell.GetNumber());
                if(!visited.Contains(nextCell) && !frontier.Contains(nextCell)) 
                {
                    nextCell.SetPrevious(current);
                    nextCell.SetDistTo(current.GetDistTo() + 1);
                    nextCell.SetDistFrom(calculateH(current.GetPosition(), end.GetPosition()));

                    frontier.Add(nextCell);
                }
            }


            if(current == end)
            {
                Debug.Log("end found. Path:");
                
                while (current.GetPrevious() != null)
                {
                    Debug.Log(current.GetNumber());
                    path.Add(current.GetPosition());
                    current = current.GetPrevious();
                }
                //path.Add(current.GetPosition());      //adding the spawn position. Not needed in the Path for the enemy.
                break; 
            }
        }
        Debug.Log("path found. revercing it.");
        path.Reverse();

        
        Debug.Log("returning path: " + path);
        return path;
    }
    public static Grid_Cell getClosestCell(List<Grid_Cell> frontier)
    {
        Grid_Cell closestCell = frontier[0];

        for(int i = 1; i < frontier.Count; i++)
        {
            if(closestCell.GetDistTo() + closestCell.GetDistFrom() > frontier[i].GetDistTo() + frontier[i].GetDistFrom())
            {
                closestCell = frontier[i];
            }
        }

        return closestCell;
    }


    public static int calculateH(Vector3 current, Vector3 goal)
    {
        int huristic = (int)Mathf.Abs(current.x - goal.x) + (int)Mathf.Abs(current.y - goal.y);
        return huristic;
    }
}
