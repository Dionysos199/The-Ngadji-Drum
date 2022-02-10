using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cirlce : MonoBehaviour
{

    float timeCounter = 0.0f;

    float speed;
    float width;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        width = 5;
        height =3;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime*speed;
        float x = Mathf.Cos(timeCounter)*width;
        float y = 3;
        float z = Mathf.Sin(timeCounter) * height; 

        transform.position = new Vector3(x, y, z);
    }
}
