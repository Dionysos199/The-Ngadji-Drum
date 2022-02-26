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


    bool exploded=false;

    public GameObject danceMusic;

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
        if (drumhitSound & gameManager.Instance.Phase == "Phase3")// we check if we are in the 3rd phase 
        {
            drumhitSound.GetSpectrumData(audioData, 0, FFTWindow.Blackman);
            //Fast Fourier Transformation and stores the amplitudes of the different frequencies in the array audiodata
            shakeAllParts();
        }
        if (exploded )
        {
            if (!Outside.activeSelf)
            {
                Outside.SetActive(true);
                GameObject floor = GameObject.FindGameObjectWithTag("Floor");
                floor.SetActive(false);
            }
        }
    }
    public void addCrackingSound()
    {
        if (gameManager.Instance.Phase == "Phase3")
        {
            StartCoroutine(instantiateCrackingSound());
        }
    }
    IEnumerator instantiateCrackingSound()
    {
        foreach (var item in wallsEmittingCrackingSound)
        {
            AudioClip cracking = soundManager.soundsArray[(int)soundManager.sounds.CRACKINGSOUND];//used enum
            soundManager.instantiateSound(item.transform.position, cracking, .5f, cracking.length);

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }


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
                shake(part, Random.Range(.1f, Mathf.PI), audioData[n] * n / 5);
            }
        }

    }

    void shake(Transform _object, float _frequency, float _amplitude) //the amplitude is fed from the audio sampled data in shakeall
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

    // This function is a listener to the hitdrum UnityEvent and will be called when Phase is reached 
    public void Explode(float hitForce, Vector3 u, int n)
    {

        if (hitForce > explosionThreshold & gameManager.Instance.Phase == "Phase4")
        {
            StartCoroutine(manySteps());
        }
    }

    // this coroutine is called right befor the explosion effect and increases dramatically the ambient light 
    IEnumerator lightAmbientIncrease()
    {
        while (true)
        {
            RenderSettings.ambientIntensity += .5f;

            yield return new WaitForSeconds(.05f);

        }
    }
    IEnumerator decreaseAmbientLight()
    {
        while (true)
        {

            RenderSettings.ambientIntensity -= 3f;

            yield return new WaitForSeconds(.05f);
        }
    }
    //bool ambientIntensityIsNull()
    //{
    //        return (RenderSettings.ambientIntensity <= 0);
    //}
    IEnumerator manySteps()
    {
        // step one Make the lions roar 
        //Increase the lights dramattically 
        AudioClip lionsRoar = soundManager.soundsArray[(int)soundManager.sounds.LionsRoar];
        soundManager.instantiateSound(Vector3.zero, lionsRoar, 1, lionsRoar.length);
        StartCoroutine(lightAmbientIncrease());
        yield return new WaitForSeconds(2);

        //step 2 decrease the lights again
        StartCoroutine(decreaseAmbientLight());
        yield return new WaitForSeconds(.4f);


        // step3 
        //disactivate all the museum objects for performance sake before explosion
        //After many tests. I can confirm that adding more than a thousand collider and rigid bodies on the scene
        // will definetly cause the pc to hang up. It super performance demanding
        toDisActivate.SetActive(false);
        yield return new WaitForSeconds(.2f);

        //step 4
        //we stop the background women chanting and dancing music
        danceMusic.GetComponent<AudioSource>().Stop();
        // explode
        explosion();

        yield return new WaitForSeconds(6);

        exploded = true;

        danceMusic.GetComponent<AudioSource>().Play();
        Debug.Log("exploded" + exploded);

    }

    void explosion()
    {

        ObjectToExplode.SetActive(true);
        // after setting disactive all the museum parts
        // we set to true the setactive of few fractured wall
        // creating the illusion that the museum exploded 

        Transform[] toExplodeChildren = ObjectToExplode.GetComponentsInChildren<Transform>();

        // run through all the children of the of the fratured wall object called objectToExplode
        // we add rigidBodies to all the pieces and box collider
        //And SYRPRISINGLY that's enough
        // the box colliders will repell each other with such a force resulting in a spectacular explosion

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

        // adds the fall down and crumble of the museum sound effect
        AudioClip crumble = soundManager.soundsArray[(int)soundManager.sounds.CRUMBLEDOWN];
        soundManager.instantiateSound(Vector3.zero, crumble, 1, crumble.length);
    }
}
