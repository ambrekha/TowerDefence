using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> queueTowers = new Queue<Tower>(); // empty queue to add towers

    public void AddTower(Waypoint baseWaypoint)
    {
        int numTowers = queueTowers.Count;

        if(numTowers < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }
    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity); // no rotation et no change of the position
        newTower.transform.parent = towerParentTransform.transform;
        baseWaypoint.isPlaceable = false; // towers can't be on the same block

        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        // add tower to the queue
        queueTowers.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        var oldTower = queueTowers.Dequeue(); // removes oldest tower off of the queue
        oldTower.baseWaypoint.isPlaceable = true;
        baseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = baseWaypoint;
        oldTower.transform.position = baseWaypoint.transform.position;

        // take bottom tower off queue
        queueTowers.Enqueue(oldTower);
    }
}
