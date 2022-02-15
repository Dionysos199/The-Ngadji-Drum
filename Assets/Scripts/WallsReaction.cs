using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsReaction : MonoBehaviour

{
    public GameObject toDisActivate;
    public GameObject ObjectToExplode;
    Rigidbody rb;
    public float MaxLifeTime;
    public float explosionThreshold;



  
    public void Explode(float hitForce, Vector3 u, int n)
    {
        if(hitForce>explosionThreshold)
        StartCoroutine(twoSteps());

    }
    IEnumerator twoSteps()
    {

        Debug.Log("explosion");
        toDisActivate.SetActive(false);

        float explosionMoment = Time.time;


        yield return new WaitForEndOfFrame();

        ObjectToExplode.SetActive(true);
        Transform[] toExplodeChildren = ObjectToExplode.GetComponentsInChildren<Transform>();
        List<GameObject> childObjects = new List<GameObject>();
        foreach (Transform child in toExplodeChildren)
        {
            childObjects.Add(child.gameObject);
        }
        foreach (GameObject gameObject in childObjects)
        {
            if (!gameObject.GetComponent<Rigidbody>())
            {
                gameObject.AddComponent<Rigidbody>();
                gameObject.AddComponent<BoxCollider>();
                float lifeTime = Random.Range(4, MaxLifeTime);
                Destroy(gameObject, lifeTime);
            }
        }

    }
}



