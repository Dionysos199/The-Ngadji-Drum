using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootsShaderScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] material;

    public float resolution;
    public float increment=.2f;


    public float appearingSpeed;
   
    AudioSource growSound;


    private IEnumerator coroutine;


    void Start()
    {
        growSound = GetComponent <AudioSource>();

        material[0].SetFloat("Vector1_7C536670", 1);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void appear(float a, Vector3 b, int index)
    {

        index = index % material.Length;

        Debug.Log("index" + index);
        StopAllCoroutines();

        float value = material[index].GetFloat("Vector1_7C536670");
        if (value > 0)
        {
            growSound.Play();
        }
        coroutine = WaitAndPrint(1 / resolution, 0, appearingSpeed, index);
        if (coroutine!= null)
        StartCoroutine(coroutine);
    }


    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime, float time,float m, int index)
    {
        float value = material[index].GetFloat("Vector1_7C536670");
        Debug.Log("index "+index);
        while(true)
        {
            time+=increment;
            value = 1-Mathf.Exp(-m*time)*m*time*2.7f;
            material[index].SetFloat("Vector1_7C536670", value);
           
            yield return new WaitForSeconds(waitTime);
        }
    }
}
