using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualReaction : MonoBehaviour
{
    // Start is called before the first frame update
    public Material reactiveMaterial;

    public Material stickreactMat;

    public float drumRadius;

    public soundManager soundManager;

    [SerializeField] int m;
    [SerializeField] float amp;
    bool withinDrumLength;
    AudioSource audioSource;
    public GameObject box;


    void Start()
    {
        //drum.GetComponent<Renderer>().material = reactiveMaterial;
        reactiveMaterial.DisableKeyword("_Emission");
        reactiveMaterial.SetColor("_EmissionColor", 0*Color.grey);
        stickreactMat.SetColor("_EmissionColor", 0 * Color.grey);
        audioSource = GetComponent<AudioSource>();

        foreach (var material in artefactsMaterials)
        {
            material.SetColor("_EmissionColor", 0* Color.white);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            audioSource.Play();
            withinDrumLength = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            withinDrumLength = false;

            audioSource.Stop();
        }
    }

    float expFn(float dist)
    {
        return Mathf.Exp(-m* dist) * (m*dist )*amp*Mathf.Exp(1);
    }
  
    public void glow(List<Vector3> positions, bool inside)
    {
        Debug.Log("inside collider"+ withinDrumLength);
        if (withinDrumLength)
        {

            Vector3 forwardVector = Quaternion.Euler(0, 0, 0) * Vector3.forward;

            float distFromRHand = Vector3.Cross(forwardVector, transform.position - positions[1]).magnitude - drumRadius;
            float distFromLHand = Vector3.Cross(forwardVector, transform.position - positions[0]).magnitude - drumRadius;
            Debug.Log("distance  " + distFromLHand);
            reactiveMaterial.EnableKeyword("_Emission");

            float intensity = expFn(distFromLHand) + expFn(distFromRHand);


            Debug.Log("intensity" + intensity);
            reactiveMaterial.SetColor("_EmissionColor", intensity/5 * Color.white);

            Light stickLight = box.GetComponent<Light>();
            stickLight.intensity = intensity+.05f;

            stickreactMat.SetColor("_EmissionColor",3* intensity * Color.white);
            audioSource.volume = intensity;

        }



    }


    /*public Material[] archive;
    public GameObject drum;
    public void displayHistory(float a, Vector3 b, int index)
    {
        index = index % archive.Length;
        drum.GetComponent<Renderer>().material=archive[index];

    }*/


    public Material[] artefactsMaterials;
    float dampen=1/2 ;
    public void glowArtefacts(float hitForce, Vector3 u, int numberOfHits)
    {
        float pitch = Vector3.Distance(u, gameManager.Instance.hideCenter.position);
        Debug.Log(" yihi " + pitch);
        if (numberOfHits>2)
        {
            //Color emissiveColor = new Color(pitch, 1, 1);
            foreach (var material in artefactsMaterials)
            {
                Debug.Log("youhou"+ hitForce);
                StartCoroutine(glowFadeOut(hitForce/10,-1,.2f, material));
            }
        }
    }
    IEnumerator glowFadeOut(float peak,float low, float speed, Material material)
    {
        for (float alpha = peak; alpha >= low; alpha -= speed)
        {

            material.SetColor("_EmissionColor", alpha * Color.white); 

            yield return null;
        }
    }

}

