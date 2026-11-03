using UnityEngine;

// Летит прямо вдоль своей синей оси и исчезает через несколько секунд.
public class Shell : MonoBehaviour
{
    public float speed = 40f;
    public float lifeSeconds = 4f;

    void Start()
    {
        Destroy(gameObject, lifeSeconds);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
