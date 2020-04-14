using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public bool isExplored = false;
    public Waypoint exploredFrom;
    public bool isPlaceable = true; // we can place a tower on the block

    [SerializeField] Tower towerPrefab;
    
    Vector2Int gridPos;

    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }

    // method to know where the mouse is on the scene
    private void OnMouseOver()
    {
        // detect mouse click
        if(Input.GetMouseButtonDown(0)) // left click
        {
            if(isPlaceable)
            {
                Instantiate(towerPrefab, transform.position, Quaternion.identity); // no rotation et no change of the position
                isPlaceable = false; // towers can't be on the same block
            }
            else
            {
                print("Isn't placeable.");
            }
        }
    }

}
