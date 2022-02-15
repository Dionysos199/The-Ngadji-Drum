using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsReaction : MonoBehaviour

{
    public GameObject toDisActivate;
    public GameObject ObjectToExplode;
    public GameObject toShake;

    Rigidbody rb;
    public float MaxLifeTime;
    public float explosionThreshold;

    public GameObject[] wallsEmittingCrackingSound;
    public soundManager soundManager;

    public GameObject Outside;


    public float[] audioData = new float[512];

    [HideInInspector] public AudioSource drumhitSound;
    //this sound should be provided in runtime each time the drum is hit
    //the sound manager has a function called playdrumhit which is a listener
    //to the event drumhit and is called each time our drum stick hits the drum
    //the sound manager assigns the new audiosource to this class
    // this audiosource audioclip will be sampled and the sampling is used to animate 
    // the different fractures of the wall like shaking 



    private void Update()
    {
        //sampling
        if (drumhitSound & gameManager.Instance.Phase == "Phase3")
        {

            drumhitSound.GetSpectrumData(audioData, 0, FFTWindow.Blackman);
            shakeAllParts(); 
        

        }
    }
    public void addCrackingSound()
    {
        if ( gameManager.Instance.Phase == "Phase3")
        {
            foreach (var item in wallsEmittingCrackingSound)
            {
                AudioClip cracking = soundManager.soundsArray[(int)soundManager.sounds.CRACKINGSOUND];
                soundManager.instantiateSound(item.transform.position, cracking, cracking.length);
            }
        }
    }
    IEnumerator instantiateCrackingSound()
    {

     
        yield return new WaitForSeconds(Random.Range(.1f,.3f));
    }
    void shakeAllParts()
    {
       
            int n = 0;
            Transform[] toShakeChildren = toShake.GetComponentsInChildren<Transform>();
            List<GameObject> childObjects = new List<GameObject>();

            foreach (Transform part in toShakeChildren)
            {
                n++;
                if (part)
                {
                    shake(part, 3.0f, audioData[n]*n);
                }
            }
        
    }

    void shake(Transform _object, float _frequency, float _amplitude)
    {
        float x = _object.transform.position.x + Mathf.Sin(Time.time * _frequency) * _amplitude * Random.Range(-1f, 1f);
        float y = _object.transform.position.y;
        float z = _object.transform.position.z + Mathf.Cos(Time.time * _frequency) * _amplitude * Random.Range(-1f, 1f);
        _object.transform.position = new Vector3(x, y, z);


        //float x = Mathf.Sin(Time.time * _frequency) * _amplitude;
        //float y = 1;
        //float z = Mathf.Cos(Time.time * _frequency) * _amplitude;
        //_object.transform.localScale = new Vector3(Mathf.Abs(x), Mathf.Abs(y),Mathf.Abs(z));


        //_object.transform.localRotation = Quaternion.Euler(20*x, y,40* z); 

    }

    public void Explode(float hitForce, Vector3 u, int n)
    {

        if (hitForce > explosionThreshold & gameManager.Instance.Phase == "Phase4")
        {
            StartCoroutine(twoSteps());
            AudioClip crumble = soundManager.soundsArray[(int)soundManager.sounds.CRUMBLEDOWN];
            soundManager.instantiateSound(Vector3.zero, crumble, crumble.length);
        }
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
        yield return new WaitForSeconds(4);
        Debug.Log("now it's over");
        Outside.SetActive(true);
    }
}



