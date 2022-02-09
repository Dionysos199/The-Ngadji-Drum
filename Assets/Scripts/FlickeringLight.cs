using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    public bool isFlickering = false;
    //public bool isHit = false;
    public float timeDelay;

    void Update()
    {
        if(isFlickering == false /*&& isHit == true*/)
        {
            StartCoroutine(FlickingLight());
        }
    }

    IEnumerator FlickingLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.5f, 1.3f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(1.0f, 2.5f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isHit = true;
            
    //    }
    //}
}
