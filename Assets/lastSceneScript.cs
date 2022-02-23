using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lastSceneScript : MonoBehaviour
{
    public Light[] directionalLights;
    public AudioSource speech;
    public Transform LastPos;
    private void Update()
    {
       

            if (directionalLights[0].intensity < .1)
            {
                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                Debug.Log("Player");
                Player.transform.position = LastPos.transform.position;
            }
    }
    public void returnToMuseum()
    {
        float intensity1 = directionalLights[0].intensity;
        float intensity2 = directionalLights[1].intensity;
        StartCoroutine(Blacken(intensity1, 0, .01f, directionalLights[0]));
        StartCoroutine(Blacken(intensity2, 0, .01f, directionalLights[1]));

        Debug.Log("intensities" + intensity1 + "  " + intensity2);
        
    }
    public IEnumerator Blacken(float peak, float low, float speed,Light light)
    {

        for (float alpha = light.intensity; alpha >= low; alpha -= speed)
        {
            light.intensity = alpha;

            speech.volume = alpha;
            yield return null;
        }
    }
    
}
//if (intensity1 < .2 || intensity2 < .2)
//{
//    GameObject Player = GameObject.FindGameObjectWithTag("Player");
//    Debug.Log("Player");
//    Player.transform.position = LastPos.transform.position;
//}