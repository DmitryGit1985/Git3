using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public Vector3 place1;
    public Vector3 place2;
    public float speed;
    public Vector3 direction1;
    private Vector3 direction2;
    private Vector3 currentDirection;

    //Почему если создаешь новые вектора в поле класса, а не в старте перестает работать?

    void Start()
    {

        speed = 30f;
        place1 = new Vector3(0.0f, 2.5f, 0.0f);
        place2 = new Vector3(7.0f, 9.5f, 14.0f);
        direction1 = new Vector3(1.0f, 2.0f, 1.0f);
        direction2 = direction1 * -1;
        currentDirection = direction1;

    }
    void Update()
    {
        if (transform.position.x >= place2.x&& transform.position.y >= place2.y&&transform.position.z >= place2.z)
        {
            currentDirection = direction2;
        }
        if (transform.position.x <= place1.x&& transform.position.y <= place1.y&& transform.position.z <= place1.z)
        {
            currentDirection = direction1;
        }
        transform.Translate(currentDirection * speed * Time.deltaTime);
    }

    }

