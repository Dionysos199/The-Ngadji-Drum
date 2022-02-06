using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube: MonoBehaviour
{
      [SerializeField]
    private float _amplitude = 0.03f;

     [SerializeField]
    private float _frequency = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time * _frequency) * _amplitude * Random.Range(-1f,1f);
        float y = transform.position.y;
        float z = Mathf.Cos(Time.time * _frequency) * _amplitude * Random.Range(-1f,1f);

        transform.position = new Vector3(x,y,z);


    }
}
