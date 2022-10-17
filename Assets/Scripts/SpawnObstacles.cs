using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject obstaclePrefab = null;

    [SerializeField]
    private Vector3 center = new Vector3(0,0,0);

    [SerializeField]
    private Vector3 size = new Vector3(10, 10, 10);

    [SerializeField]
    private int amount = 10;

    private int _counter = 0;

    void Start()
    {
        while (_counter < amount)
        {
            Spawn();
            _counter++;
        }
    }

    private void Spawn()
    {
        float x = Random.Range(-size.x /2, size.x / 2);
        float y = Random.Range(-size.y / 2, size.y / 2);
        float z = Random.Range(-size.z / 2, size.z / 2);

        Vector3 pos = center + new Vector3(x,y,z);

        Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,1,1, 0.1f);
        Gizmos.DrawCube(center, size);
    }
}
