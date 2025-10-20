using UnityEngine;

public class LocationSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] locationPrefabs;
    [SerializeField] GameObject firstLocation;
    float locationLength = 15f;
    private float playerPositionZ;
    private float lastSpawnPositionZ;
    public int additionalLocationsCount = 10;
    public Transform player;
    [SerializeField] GameObject bounusPrefab;
    void Start()
    {
        lastSpawnPositionZ = firstLocation.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > lastSpawnPositionZ - locationLength * additionalLocationsCount)
        {
            GenerateAdditionalLocations();
            if (Random.value < 0.3f)
            {
                Instantiate(bounusPrefab, player.position + new Vector3(0,Random.Range(1,3),50), Quaternion.Euler(0, 0, 0));
            }
        }
    }
    void GenerateAdditionalLocations()
    {
        for (int i = 0; i < additionalLocationsCount; i++)
        {
            lastSpawnPositionZ = lastSpawnPositionZ + locationLength;
            int randomIndex = Random.Range(0, locationPrefabs.Length);
            GameObject selectedPrefab = locationPrefabs[randomIndex];
            GameObject newLocation = Instantiate(selectedPrefab, new Vector3(0, 0, lastSpawnPositionZ), Quaternion.identity);
        }
    }
}
