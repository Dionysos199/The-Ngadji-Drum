using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;


public class testingCoroutine : MonoBehaviour
{


    public AudioSource playSound;
    public GameObject danceMusic;
    public lastSceneScript lastscene;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            playSound.Play();
            danceMusic.GetComponent<AudioSource>().volume = .2f;
            StartCoroutine(lastScene());

        }
    }

    IEnumerator lastScene()
    {

        yield return new WaitForSeconds(20f);
        Debug.Log("go now");
        lastscene.returnToMuseum();


    }
    //IEnumerator Fade()
    //{
    //    for (float alpha = 10f; alpha >= 0; alpha -= 0.1f)
    //    {

    //        cubeMateriel.SetColor("_EmissionColor", alpha * Color.white);
    //        yield return null;
    //    }
    //}
}
