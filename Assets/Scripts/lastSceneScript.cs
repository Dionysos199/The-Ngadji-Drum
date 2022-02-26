using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lastSceneScript : MonoBehaviour
{
    public Light[] directionalLights;
    public AudioSource [] sounds;
    public GameObject toActivate;
    public Transform LastPos;
    bool finalSceneDone=false;
    private void Update()
    {
        // after seconds from listening to the old man 

        //checks constantly if the light intensity in the outside scene is less than .1
        //if it drops below .1 we move the player inside the museum again 
        // and we end the experience 

        if (directionalLights[0].intensity < .1)
            if(finalSceneDone== false)//only if the last scene has never been played this code will executed
            {

            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("Player");
            Player.transform.position = LastPos.transform.position;
             switchScenes();
             finalSceneDone = true;
        }
    }
    void  switchScenes()
    {
        outsideManger outside = FindObjectOfType<outsideManger>();
        outside.gameObject.SetActive(false);
        toActivate.SetActive(true);
    }
    public void returnToMuseum()
    {
        float intensity1 = directionalLights[0].intensity;
        float intensity2 = directionalLights[1].intensity;
        StartCoroutine(Blacken(intensity1, 0, .01f, directionalLights[0]));
        StartCoroutine(Blacken(intensity2, 0, .01f, directionalLights[1]));
        
        Debug.Log("intensities" + intensity1 + "  " + intensity2);
        
    }

    //coroutine to fade out any value in general 
    // in this case added one argument, the light, to avoid defining the coroutine many times for each light
    public IEnumerator Blacken(float peak, float low, float speed,Light light)
    {

        for (float alpha = light.intensity; alpha >= low; alpha -= speed)
        {
            light.intensity = alpha;

            foreach (var sound in sounds)
            {
                sound.volume = alpha;
            }
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