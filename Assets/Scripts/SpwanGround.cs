using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanGround : MonoBehaviour
{
    public GroundSO groundSO;
    public GameObject prefabParent; // Parent object for the instantiated prefabs
    public bool creatingGround = false;
    public float instantiationInterval = 4f; // Time interval between instantiations
    public float gapBetweenPrefabs = 18f; // Gap between prefabs along the x-axis

    private List<GameObject> instantiatedGrounds = new List<GameObject>(); // List to store instantiated ground objects

    private void Start()
    {
        // Start spawning ground immediately
        StartCoroutine(GenerateGrounds());
    }

    IEnumerator GenerateGrounds()
    {
        // Ensure groundSO is not null and has grounds
        if (groundSO != null && groundSO.grounds.Count > 0)
        {
            int index = 0; // Start with the first index

            while (true) // Continue indefinitely
            {
                // Instantiate the prefab at the current index
                GameObject newGround = Instantiate(groundSO.grounds[index], transform.position, Quaternion.identity);

                // Add the instantiated ground object to the list
                instantiatedGrounds.Add(newGround);

                // Set the parent object for organization (if specified)
                if (prefabParent != null)
                {
                    newGround.transform.parent = prefabParent.transform;
                }

                // Move the newly instantiated prefab along the x-axis
                transform.position += Vector3.right * gapBetweenPrefabs;

                // Wait for a moment before instantiating the next prefab
                yield return new WaitForSeconds(instantiationInterval);

                // Move to the next index (wrapping around if necessary)
                index = (index + 1) % groundSO.grounds.Count;
            }
        }
    }

    // Called when the player hits a finish tag
    public void OnFinishTagHit()
    {
        // Destroy previously instantiated ground objects
        foreach (GameObject ground in instantiatedGrounds)
        {
            Destroy(ground);
        }

        // Clear the list of instantiated ground objects
        instantiatedGrounds.Clear();
    }
}
