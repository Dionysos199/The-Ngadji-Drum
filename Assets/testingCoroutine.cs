using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingCoroutine : MonoBehaviour
{

    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(rotateCube());
    }
    IEnumerator rotateCube()
    {
        
        transform.rotation = Quaternion.Euler(0, 10, 0);
        yield return new WaitForSeconds(1.0f);

        transform.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(1.0f);

        transform.position += transform.forward;
        yield return new WaitForSeconds(1.0f);


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
