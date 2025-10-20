using UnityEngine;

public class Cycle : MonoBehaviour
{
    [SerializeField] int[] numbers;
    void Start()
    {
        for (int i = 0; i < numbers.Length; i++)
        { 
            Debug.Log(numbers[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
