using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class SailorsEvent : UnityEvent<int>
{
}

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
    public float patrolSpeed;
    public float turnSpeed;
    public List<Vector3> waypoints;
    public float minWaypointDistance = 0.1f;
    public GameObject[] attackPoint;
    public SailorsEvent sailorEvent;

    private Vector3 currentDestination;
    private int maxWaypoint;
    private NavMeshAgent nav;

    private int rudderSailor = 0;
    private int nestSailor = 0;
    public int cannonLeft = 0;
    public int cannonRight = 0;
    public int cannonFront = 0;

    public GameObject flagObjects;
    public GameObject frontCannon;


    private void Awake()
    {
        instance = this;
        sailorEvent.AddListener(spawnSailor);
    }

    private void Start()
    {
        turnSpeed = GameManager.instance.TurnSpeedDic[tag];
        patrolSpeed = GameManager.instance.SpeedDic[tag];
    }

   
    private void Update()
    {      
        MouseController();
        Patrolling();
    }

    public void reset()
    {
        rudderSailor = 0;
        nestSailor = 0;
        cannonLeft = 0;
        cannonRight = 0;
        cannonFront = 0;
        waypoints.Clear();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.position = new Vector3(0f, 0.81f, 0f);
        transform.rotation = Quaternion.identity;
    }

    public void MouseController()
    {
        if (InputManager.instance.getSelection() != Vector3.zero && rudderSailor == 1)
        {
            waypoints.Clear();
            waypoints.Add(InputManager.instance.getSelection());
        }
    }

    private void spawnSailor(int sailorType)
    {
        switch (sailorType)
        {
            case 1: if(rudderSailor < 1) rudderSailor++; break;
            case 2: if (nestSailor < 1) nestSailor++; CameraController.instance.nest = true; break;
            case 3: if (cannonLeft < 2) cannonLeft++; break;
            case 4: if (cannonRight < 2) cannonRight++; break;
            case 5: if (cannonFront < 2) cannonFront++; break;
        }
    }

    public void Patrolling()
    {
        if (waypoints.Count > 0)
        {
            // Create two Vector3 variables, one to buffer the ai agents local position, the other to
            // buffer the next waypoints position
            Vector3 tempLocalPosition;
            Vector3 tempWaypointPosition;

            // Agents position (x, set y to 0, z)
            tempLocalPosition = transform.position;
            tempLocalPosition.y = 0f;

            // Current waypoints position (x, set y to 0, z)
            tempWaypointPosition = waypoints[0];
            tempWaypointPosition.y = 0f;


            // Is the distance between the agent and the current waypoint within the minWaypointDistance?
            if (Vector3.Distance(tempLocalPosition, tempWaypointPosition) <= minWaypointDistance)
            {
                waypoints.Remove(waypoints[0]);
            }

            // Set the destination for the agent
            // The navmesh agent is going to do the rest of the work
            if (waypoints.Count > 0)
            {
                transform.position += transform.forward*patrolSpeed * Time.deltaTime;
                Vector3 targetDir = waypoints[0] - transform.position;
                float step = Time.deltaTime / Mathf.PI;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step * turnSpeed , 0.0f);
                transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x,0,newDir.z));

                /*transform.position = new Vector3(Mathf.Clamp(transform.position.x, GameManager.instance.minX, GameManager.instance.maxX),
                                                            transform.position.y,
                                                              Mathf.Clamp(transform.position.z, GameManager.instance.minZ, GameManager.instance.maxZ));*/
            

             }
        }
    }
}
