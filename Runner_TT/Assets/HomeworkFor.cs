using UnityEngine;

public class HomeworkFor : MonoBehaviour
{
    int number = 0;
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            number++;
            Debug.Log(number);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
