using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualReaction : MonoBehaviour
{
    // Start is called before the first frame update
    public Material reactiveMaterial;

    public float drumRadius;



    [SerializeField] int m;
    [SerializeField] float amp;

    void Start()
    {
        drum.GetComponent<Renderer>().material = reactiveMaterial;
        reactiveMaterial.DisableKeyword("_Emission");
        reactiveMaterial.SetColor("_EmissionColor", Color.grey);
        
    }

    float expFn(float dist)
    {
        return Mathf.Exp(-m* dist) * (m*dist )*amp*Mathf.Exp(1);
    }
  
    public void glow(List<Vector3> positions, bool inside)
    {

        Vector3 forwardVector = Quaternion.Euler(0, 45, 0) * Vector3.forward;
     
        float distFromRHand = Vector3.Cross(forwardVector, transform.position - positions[1]).magnitude - drumRadius;
        float distFromLHand = Vector3.Cross(forwardVector, transform.position - positions[0]).magnitude - drumRadius;
        Debug.Log("distance  " + distFromLHand);
        reactiveMaterial.DisableKeyword("_Emission");

        float intensity = expFn(distFromLHand)+expFn(distFromRHand);

        
        Debug.Log("intensity"+intensity);
        reactiveMaterial.SetColor("_EmissionColor", intensity * Color.yellow);
    }


    public Material[] archive;
    public GameObject drum;
    public void displayHistory(float a, Vector3 b, int index)
    {
        index = index % archive.Length;
        drum.GetComponent<Renderer>().material=archive[index];

    }
}

