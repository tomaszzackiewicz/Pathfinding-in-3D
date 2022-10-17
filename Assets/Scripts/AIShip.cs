using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShip : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private float rotationalDamp = 0.5f;

    [SerializeField]
    private float movementSpeed = 2.0f;

    [SerializeField]
    private float rotationSpeed = 50.0f;

    [SerializeField]
    private float raycastOffset = 2.5f;

    [SerializeField]
    private float detectionDistance = 20.0f;

    [SerializeField]
    private float sphereRadius = 2.0f;

    [SerializeField]
    public LayerMask layerMask;

    private float currentHitDistance = 0;
    private Vector3 front = Vector3.zero;

 
    void Update()
    {
        Pathfinding();
        //Turn();
        Move();
    }

    private void Turn()
    {
        Vector3 pos = target.position + new Vector3(5,0,0) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    private void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    private void Pathfinding()
    {
        RaycastHit hit;

        front = transform.position;

        bool isHit = Physics.SphereCast(front, sphereRadius, transform.forward, out hit, detectionDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        //bool isHit = Physics.Raycast(front, transform.forward, out hit, detectionDistance)
        //Debug.DrawRay(front, transform.forward * detectionDistance, Color.red);
        if (isHit)
        {
            currentHitDistance = hit.distance;
            ObstacleController obstacleController = hit.collider.GetComponent<ObstacleController>();
            if (obstacleController)
            {
                Transform point = obstacleController.FindTheNearestPoint(hit.point);
                if (point)
                {
                    Debug.Log(point.name);
                    ClosestPoint closestPoint = point.GetComponent<ClosestPoint>();
                    if (closestPoint)
                    {
                        CheckDirection(closestPoint.ClosestPointType);
                    }
                }
            }
        }
        else
        {
            currentHitDistance = detectionDistance;
            CheckDirection(ClosestPointType.None);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Debug.DrawLine(front, front + transform.forward * currentHitDistance);
    //    Gizmos.DrawWireSphere(front + transform.forward * currentHitDistance, sphereRadius);
    //}

    private void CheckDirection(ClosestPointType closestPointTypeParam)
    {
        Vector3 rayCastOffset = Vector3.zero;
        switch (closestPointTypeParam)
        {
            case ClosestPointType.Up:
                rayCastOffset -= Vector3.up;
                break;
            case ClosestPointType.Down:
                rayCastOffset += Vector3.up;
                break;
            case ClosestPointType.Right:
                rayCastOffset -= Vector3.right;
                break;
            case ClosestPointType.Left:
                rayCastOffset += Vector3.right;
                break;
            default:
                rayCastOffset = Vector3.zero;
                break;

        }

        if (rayCastOffset != Vector3.zero)
        {
            transform.Rotate(rayCastOffset * rotationSpeed * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    }

    ////////////////////////////////////////////////////////////


    public Vector3 sourceObject;
    public Collider targetCollider;

    // Update is called once per frame
    void ClosestPoint()
    {
        //Method 1
        Vector3 closestPoint = targetCollider.ClosestPoint(sourceObject);

        //Method 2
        Vector3 closestPointInBounds = targetCollider.ClosestPointOnBounds(sourceObject);

        //Method 3
        Vector3 pos = targetCollider.transform.position;
        Quaternion rot = targetCollider.transform.rotation;
        Vector3 closestPointCollider = Physics.ClosestPoint(sourceObject, targetCollider, pos, rot);
    }

    Vector3 ClosestPointOnMeshOBB(MeshFilter meshFilter, Vector3 worldPoint)
    {
        // First, we transform the point into the local coordinate space of the mesh.
        var localPoint = meshFilter.transform.InverseTransformPoint(worldPoint);

        // Next, we compare it against the mesh's axis-aligned bounds in its local space.
        var localClosest = meshFilter.sharedMesh.bounds.ClosestPoint(localPoint);

        // Finally, we transform the local point back into world space.
        return meshFilter.transform.TransformPoint(localClosest);
    }

    private void OldStuff()
    {
        //Vector3 left = transform.position - transform.right * raycastOffset;
        //Vector3 right = transform.position + transform.right * raycastOffset;
        //Vector3 up = transform.position + transform.up * raycastOffset;
        //Vector3 down = transform.position - transform.up * raycastOffset;

        //Debug.DrawRay(left, transform.forward * detectionDistance /2, Color.yellow);
        //Debug.DrawRay(right, transform.forward * detectionDistance /2, Color.yellow);
        //Debug.DrawRay(up, transform.forward * detectionDistance /2, Color.yellow);
        //Debug.DrawRay(down, transform.forward * detectionDistance /2, Color.yellow);

        //if (Physics.Raycast(left, transform.forward, out hit, detectionDistance /2))
        //{

        //    if (lockLeft) return;
        //    UnLockLeft();
        //    rayCastOffset += Vector3.right;
        //}
        //if (Physics.Raycast(right, transform.forward, out hit, detectionDistance /2))
        //{

        //    if (lockRight) return;
        //    UnLockRight();
        //    rayCastOffset -= Vector3.right;
        //}
        //if (Physics.Raycast(up, transform.forward, out hit, detectionDistance /2))
        //{

        //    if (lockUp) return;
        //    UnLockUp();
        //    rayCastOffset -= Vector3.up;
        //}
        //if (Physics.Raycast(down, transform.forward, out hit, detectionDistance / 2))
        //{

        //    if (lockDown) return;
        //    UnLockDown();
        //    rayCastOffset += Vector3.up;
        //}
        //else
        //{
        //    //UnLockAll();
        //}
    }
}
