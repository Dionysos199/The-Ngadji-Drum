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
      
    }

    public void flicker(float flickerForce, Vector3 u, int n )
    {
        StartCoroutine(Flickering(flickerForce));
      //  Material lightEmission = GetComponentInParent<Material[]>()[1];

    }
    IEnumerator Flickering(float FlickeringTime)
    {
        
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.5f, 1.3f*FlickeringTime);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(1.0f, 2.5f*FlickeringTime);
        yield return new WaitForSeconds(timeDelay);
       
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isHit = true;
            
    //    }
    //}
}
