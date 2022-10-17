using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    public List<Transform> pointList = new List<Transform>();

    private Transform nearestPoint;

    public Vector3 location;
    //public Collider col;


    //public void OnDrawGizmos()
    //{
       
    //    var col = GetComponent<Collider>();

    //    if (!col)
    //    {
    //        return; // nothing to do without a collider
    //    }

    //    Vector3 closestPoint = col.ClosestPoint(location);

    //    Gizmos.DrawSphere(location, 0.1f);
    //    Gizmos.DrawWireSphere(closestPoint, 0.1f);
    //}

    public Transform FindTheNearestPoint(Vector3 locationParam)
    {

        location = locationParam;
        float minimumDistance = Mathf.Infinity;
        //if (nearestEnemy != null)
        //{
        //    nearestEnemy.GetComponent<MeshRenderer>().material.color = Color.green;
        //}
        nearestPoint = null;
        foreach (Transform enemy in pointList)
        {
            float distance = Vector3.Distance(locationParam, enemy.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                nearestPoint = enemy;
            }
        }
        //nearestEnemy.GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log("Nearest Enemy: " + nearestPoint + "; Distance: " + minimumDistance);
        return nearestPoint;
    }
}
