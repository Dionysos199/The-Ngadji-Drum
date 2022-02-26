

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

[System.Serializable]
public class hitdrum : UnityEvent<float, Vector3, int>
{

}
public class drumInteraction : MonoBehaviour
{

    public hitdrum hitDrum;



    //public Transform explosionPrefab;

    public Transform stickHead;
    public lightManager lightmanager;

    Transform hidePos;
    Rigidbody stickHeadRb;
    // Start is called before the first frame update
    private InputDevice targetDevice;

    public float hitforceThreshold = 2;

    int numberOfHits;
    int phaseThreshold=5;
    int phaseNum;

    Vector3 positionOnExit;
    float distanceFromHitPos;



    bool loaded = true;
    float reloadDist=.4f;
    void Start()
    {
        stickHeadRb = stickHead.GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        distanceFromHitPos = Vector3.Distance(positionOnExit, stickHead.position);


        //to avoid hitting accidentally or repeatedly the drum
        // because it's important to keep track of the number of times the dum was hit
        // to know which animation or interaction is played 
        // and have more control on them 

        //If the drum stick is waved away from the hide a certain distance only then loaded is set to true and
        // we allow a second hit 
        if (distanceFromHitPos > reloadDist)
        {
            loaded = true;
        }
    }


    // The moment the stickhead leaves the collider we record its position
    // we need this position to keep track of the distance the stick is travelling away from the drum
    // and set the boolean value "loaded" to true
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "stick")
        {
            Debug.Log("exit collision");
          
            positionOnExit = stickHead.transform.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (loaded)
        {
            
            loaded = false; // immediatley sets the loaded value to false 


            if (collision.gameObject.tag == "stick")
            {
                Debug.Log("collision is detected");

                // gets the velocity of the stick which is similar to the hit force
                    float hitForce = stickHeadRb.GetPointVelocity(transform.TransformPoint(stickHead.position)).magnitude / 100;


                    if (hitForce > hitforceThreshold)
                    {
                        numberOfHits++;

                        gameManager.Instance.numberOfHits++;



                        if (numberOfHits % phaseThreshold == 0)//each "phaseThreshold" number of hits we pass to the next phase
                        {

                            if (phaseNum < gameManager.Instance.Phases.Length)
                            {
                                phaseNum++;

                                Debug.Log("number of Hits  " + numberOfHits + "  phaseNum " + gameManager.Instance.Phase);
                                gameManager.Instance.Phase = gameManager.Instance.Phases[phaseNum];
                            }
                        }
                        // get the speed of the drumstick the moment it hits the drum 

                        ContactPoint contact = collision.GetContact(0);
                        Vector3 hitPos = contact.point;

                        Debug.Log("hitPos" + hitPos);
                        //calculates how far from the center of the drum we are hitting to use it
                        // to control the pitch of the emitted sound


                        hitDrum?.Invoke(hitForce, hitPos, numberOfHits);
                        // lightmanager?._hitdrum.Invoke(hitForce, hitPos, numberOfHits);



                        // myMaterial.color = new Color(5 * distFromCenter, 0, 0);


                    }
            }
        }
    }
}
