using UnityEngine;

public class Test : MonoBehaviour
{
    int health = 100;
    string characterName = "Pers";
    float speed = 5.5f;
    bool canJump = true;
    [SerializeField]Vector3 direction = new Vector3(0, 0, 0);
    [SerializeField]int frame = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.localScale.x < 10)
        {
            transform.localScale += direction;
        }
    }
}
