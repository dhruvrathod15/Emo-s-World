using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPooling : MonoBehaviour
{
    public LandSO timerGroundSO;
    public int poolSize = 10;
    private Queue<GameObject> poolQueue;
    public float instantiateXPosition = 85.98f;

    private void Start()
    {
        poolQueue = new Queue<GameObject>();

        // Reset currentIndex of GroundSO to 0 before instantiating the first ground object
        timerGroundSO.ResetIndex();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject groundObject = InstantiateGroundObject();
            groundObject.SetActive(false);
            poolQueue.Enqueue(groundObject);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (poolQueue.Count > 0)
        {
            // Dequeue an object from the pool
            GameObject groundObject = poolQueue.Dequeue();
            groundObject.SetActive(true);
            return groundObject;
        }
        // If the pool is empty, instantiate a new object
        GameObject newObject = InstantiateGroundObject();
        return newObject;
    }

    // Return an object to the pool
    public void ReturnObjectToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        // Enqueue the object back into the pool
        poolQueue.Enqueue(gameObject);
    }

    private GameObject InstantiateGroundObject()
    {
        // Get the next ground prefab from the GroundSO
        GameObject prefab = timerGroundSO.GetNextPrefab();
        GameObject groundObject = Instantiate(prefab);
        groundObject.transform.position = new Vector3(instantiateXPosition, 0f, 0f);
        return groundObject;
    }
}