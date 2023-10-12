using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class A_Star
{
    public List<Vector3> findPath(Grid_Manager grid, Grid_Cell start, Grid_Cell end)
    {
        List<Vector3> path = new List<Vector3>();

        //priority queue for frontier
        ArrayList visited = new ArrayList();

        //add start to frontier

        Grid_Cell startPoint = start;
        startPoint.SetDistTo(0);

        while(true)
        {
            Grid_Cell current = frontier.poll(); //basically remove the first cell in frontier
            visited.Add(current);
            Grid_Cell[] neighbours = grid.getNeighbour(current);
            foreach (Grid_Cell i in neighbours)
            {
                if(!visited.Contains(i) && !frontier.contains(i)) 
                {
                    i.SetPrevious(current);
                    i.SetDistTo(current.GetDistTo() + 1);
                    i.SetDistFrom(calculateH(current.GetPosition(), end.GetPosition()));

                    // add i to frontier
                }
            }

            if(current == end)
            {
                while(current.GetPrevious() != null)
                {
                    path.Add(current.GetPosition());
                    current = current.GetPrevious();
                }
                break; 
            }
        }

        //reverse the path

        return path;
    }

    private int calculateH(Vector3 current, Vector3 goal)
    {
        int huristic = (int)Mathf.Abs(current.x - goal.x) + (int)Mathf.Abs(current.y - goal.y);
        return huristic;
    }
}
