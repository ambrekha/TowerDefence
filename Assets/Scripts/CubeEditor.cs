using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[SelectionBase] // so you grab the whole object and not one of its child
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>(); 
    }

    void Update()
    {
        SnapToGrid();

        UpdateLabel();

    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();

        //moving the cubes along a grid
        transform.position = new Vector3(waypoint.GetGridPos().x*gridSize, 0f, waypoint.GetGridPos().y*gridSize);
    }

    private void UpdateLabel()
    {
        //updating the label
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
