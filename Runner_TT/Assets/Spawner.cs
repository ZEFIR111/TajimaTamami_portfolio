using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    int number = 0;
    
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            if (Random.Range(0, 100) < 50)
            {
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)), Quaternion.Euler(0, 0, 0));
                number++;
            }
        }
        Debug.Log(number);
    }
}
