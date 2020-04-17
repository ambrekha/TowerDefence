using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter; // current search center
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    { // different directions the enemy can hypothetically go to
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> getPath()
    {
        // following code is to allow more than one enemy on the field
        if(path.Count == 0) // if no path
        {
            // computes path before returning it
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);
        endWaypoint.isPlaceable = false;

        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            path.Add(previous);
            previous.isPlaceable = false;
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        startWaypoint.isPlaceable = false;
        path.Reverse(); // reverse the list
    }

    private void BreadthFirstSearch()
    { // based on Breadth First Seach Algorithm
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue(); // center is the block it is currently analysing, before getting their existing neighbours
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndFound()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }

        foreach(Vector2Int direction in directions)
        {
            Vector2Int expCoordinates = searchCenter.GetGridPos() + direction; // coordinates of current neighbour
            if(grid.ContainsKey(expCoordinates))
            { // test if all neighbours exist
                QueueNewNeighbours(expCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int expCoordinates)
    {
        Waypoint neighbour = grid[expCoordinates];
        if(neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach(Waypoint waypoint in waypoints)
        {
            // detect overlapping blocks before adding to dictionnary
            if(grid.ContainsKey(waypoint.GetGridPos()))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
                //waypoint.SetTopColor(Color.red);
            }
        }
    }
}
