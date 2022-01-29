using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mandlebrot : MonoBehaviour
{
    public GameObject Object;

    private Vector2 C;
    [Range(0f, 1.0f)]
    public float Z0x;
    [Range(0f, 1.0f)]
    public float Z0y;

    private Vector2 Z0;

    [Range(0f, 1.0f)]
    public float Cx;
    [Range(0f, 1.0f)]
    public float Cy;

    Material material;
    public int length;





    GameObject[] instances;

    // Start is called before the first frame update
    void Start()
    {
        C = new Vector2(Cx, Cy);
        Z0 = new Vector2(Z0x, Z0y);
    }
    void drawMandlebrot()
    {
        
        float c=Mathf.Sin(Time.time);
        float d = Mathf.Cos(Time.time);
        Vector2[] Z = new Vector2[length];
        for (int i = 1; i < length; i++)
        {
            float x = Z[i - 1][0];
            float y = Z[i - 1][1];
            Z[i][0] = x * x - y * y + c;
            Z[i][1] = 2 * x * y + d;
            Vector3 pos = new Vector3(Z[i][0], Z[i][1], 0);
            GameObject cube=  Instantiate(Object, pos, Quaternion.identity);
          
            Destroy(cube,3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        drawMandlebrot();
    }
}
