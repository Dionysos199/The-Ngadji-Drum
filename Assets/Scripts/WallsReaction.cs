using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsReaction : MonoBehaviour

{
    Rigidbody rb;
    public float MaxLifeTime;
    public float explosionThreshold;
    private void Start()
    {

    }
    public void Explode(float a, Vector3 b, int c)
    {
        Debug.Log("Hitforce" + a);
        if (a > explosionThreshold)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            List<GameObject> childObjects = new List<GameObject>();
            foreach (Transform child in allChildren)
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

}



