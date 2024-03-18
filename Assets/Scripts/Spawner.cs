using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject[] collectibles;
    [SerializeField] private GameObject[] obstacles;

    
    void Start()
    {
        float distance = Vector3.Distance(startPoint.position, endPoint.position);
        float[] xPositions = { -2, 0, 2 };
        
        for (int i = Mathf.FloorToInt(startPoint.position.z) + 10; i < Mathf.FloorToInt(endPoint.position.z) - 5; i += 5) 
        {
            int random = Random.Range(0, 3);
            GameObject[] container;
            if (random < 1)
            {
                container = collectibles;
            }
            else 
            {
                container = obstacles;
            }
            GameObject newObj = container[Random.Range(0, container.Length)];
            Instantiate(newObj, new Vector3(xPositions[Random.Range(0, 3)], newObj.transform.position.y, (float)i), Quaternion.identity);
        }
    }

}
