using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadSegmentPrefab;
    public Transform player;

    public float roadLength = 50f;
    public int numberOfSegments = 5;
    public int maxSegments = 10;

    private List<GameObject> roads = new List<GameObject>();
    private float lastSpawnPosition = 0f;

    void Start()
    {
        lastSpawnPosition = 0f;
        for (int i = 0; i < numberOfSegments; i++)
        {
            SpawnRoadSegment();
        }
    }

    void Update()
    {
        if (player == null || roads.Count == 0)
            return;

        // Check if the player has passed the first road segment
        if (player.position.z > roads[0].transform.position.z + roadLength)
        {
            // Move the first road to the end
            GameObject firstRoad = roads[0];
            firstRoad.transform.position = new Vector3(
                0f,
                0f,
                lastSpawnPosition + roadLength
            );
            lastSpawnPosition += roadLength;

            // Reorder the list
            roads.RemoveAt(0);
            roads.Add(firstRoad);
        }
    }

    void SpawnRoadSegment()
    {
        GameObject road = Instantiate(
            roadSegmentPrefab,
            new Vector3(0f, 0f, lastSpawnPosition),
            Quaternion.identity
        );
        roads.Add(road);
        lastSpawnPosition += roadLength;
    }
}
