using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimerGroundSO", menuName = "ScriptableObjects/Timer Scene/TimerGroundSO")]
public class TimerGroundSO : SerializedScriptableObject
{
    public List<GameObject> grounds;
    private int currentIndex = 0;

    // This list will store the sequence of indices
    private List<int> sequence = new List<int> { 0, 1, 2, 3 };

    public GameObject GetNextPrefab()
    {
        // Get the next index based on the sequence
        int index = sequence[currentIndex];

        // Increment currentIndex, loop back if it exceeds the sequence length
        currentIndex = (currentIndex + 1) % sequence.Count;

        // Return the corresponding ground prefab
        return grounds[index];
    }

    // Method to reset currentIndex to 0
    public void ResetIndex()
    {
        currentIndex = 0;
    }
}
