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
        //set the emmissive materials on the drum and the stick to no emission originally 
        reactiveMaterial.SetColor("_EmissionColor", 0*Color.grey);
        stickreactMat.SetColor("_EmissionColor", 0 * Color.grey);
        audioSource = GetComponent<AudioSource>();

        foreach (var material in artefactsMaterials)
        {
            material.SetColor("_EmissionColor", 0* Color.white);
        }
    }


    // First a box trigger is place around the drum
    // triggered when the player reach around one meter from the drum
    //when triggered withinDrumLength boolean is set true
    // respectivelly it is set to false when we the trigger is exited
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

    // exponential function rapidly fades away the sound volume and the emission from the drum
    // I used Desmos online graph editor // very helpfull to visualize how you want to controll your values
    float expFn(float dist)
    {
        return Mathf.Exp(-m* dist) * (m*dist )*amp*Mathf.Exp(1);
    }
  
    //this class is a listener and is notified by the insideInfluenceArea unityEvent when our hands reach close to the surface of the drum

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

            float intensity = expFn(distFromLHand) + expFn(distFromRHand);// the effect is additive if you are using both hands
            reactiveMaterial.SetColor("_EmissionColor", intensity/5 * Color.white);//the emmisive material on the drum body is affected


            Light stickLight = box.GetComponent<Light>();
            stickLight.intensity = intensity+.05f;// the light inside the box glowing 

            stickreactMat.SetColor("_EmissionColor",3* intensity * Color.white);// the emmissive material on the stick 
           
            audioSource.volume = intensity;// the volume of the singing men sound

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

