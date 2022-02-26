using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootsShaderScript : MonoBehaviour
{
    public Material[] material;//made different copies from the same material and added each to a different material

    public float resolution; //controls how detailed and smooth the animation is 
    public float increment=.2f; // parameter that controls how fast the animation goes

    public GameObject[] trees; //array of the trees that will evolve on the scene 

    public float appearingSpeed;

    public soundManager soundManager;

    private IEnumerator coroutine;


    void Start()
    {
        foreach (var _material in material)
        {
            _material.SetFloat("Vector1_7C536670", 1); //Vector1_7C536670 is the property  in the shader graph that controls
            //how much of the tree appears
            //it's a float value between 0 and 1
            // a max value of one corresponds to total dissolving 
            // a min value of zero corresponds to complete appearance 
            // so at the beginning we set it to 1 in all the materials to make all the trees invisible at the start of the experience
        }
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void appear(float a, Vector3 b, int index)
    {
        if (gameManager.Instance.Phase=="Phase2") // if the second phase is reached 
        {
            int n = index % material.Length;

            Debug.Log("index" + n);
            //StopAllCoroutines();

            float value = material[n].GetFloat("Vector1_7C536670");
            if (value > 0)
            {
                //iterates only within the limits of the length of the trees array
                int p = index % trees.Length;
                Debug.Log("p " + p);
                //calls the fn inside the sound Manager instance attached to this object
                soundManager.playTheRootsGrowSounds(trees[p].GetComponent<Transform>());
            }
            coroutine = growTreeAnimation(1 / resolution, 0, appearingSpeed, n);
            if (coroutine != null)
                StartCoroutine(coroutine);
        }
     
    }


    // every 2 seconds perform the print()

    //coroutine for animating the dissolving/appearing trees when called 
    private IEnumerator growTreeAnimation(float waitTime, float time,float m, int index)
        //waittime is the resoltion because its how frequent the coroutine is called
        //index to know which material/ on which tree we are doing the shader effect
    {
        float value = material[index].GetFloat("Vector1_7C536670");
        Debug.Log("index "+index);
        while(true)
        {
            time+=increment;
            // value = 1-Mathf.Exp(-m*time)*m*time*2.7f;
             value = Mathf.Exp( -time); //the exponential function is perfect for fast fading out values
            //the value fades fastly to zero causing the corresponding material on the tree to appear

            material[index].SetFloat("Vector1_7C536670", value);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
