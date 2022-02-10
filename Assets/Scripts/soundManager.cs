using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject soundObject;
    public AudioClip[] soundsArray;
    AudioSource drumSound;
    public Transform hideCenter;
    public GameObject museum;
    IEnumerator coroutine; 


    private IEnumerator playMusic(float waitTime)
    {
      
        while (true)
        {
            playThePokomoSinging();
            yield return new WaitForSeconds(waitTime);
        }
    }
    void playThePokomoSinging()
    {
        GameObject pokomoSoundObject = Instantiate(soundObject, new Vector3(10, 0, 10), Quaternion.identity);
        drumSound = pokomoSoundObject.GetComponent<AudioSource>();
        drumSound.clip = soundsArray[(int)sounds.PokomoSinging];
        drumSound.Play();
        Debug.Log("pokomo song");
        Destroy(pokomoSoundObject, 3);
    }
    enum sounds {
        JAPENESEDRUMSOUND, LionsRoar, PokomoSinging, treeSound
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playDrumSound( float hitForce, Vector3 hitPos,int a)
    {

        GameObject drumSoundObject = Instantiate(soundObject, hitPos, Quaternion.identity);
        drumSound = drumSoundObject.GetComponent<AudioSource>();
        
     

        drumSound.clip = soundsArray[(int)sounds.JAPENESEDRUMSOUND];
        Debug.Log("now Playing"+drumSound.clip);

        float distFromCenter = Vector3.Distance(hitPos, hideCenter.position);
        Debug.Log("hide center" + hideCenter);
        drumSound.pitch = distFromCenter*2;
        drumSound.volume = hitForce*2;
        drumSound.Play();
        Destroy(drumSoundObject, drumSound.clip.length);
    }
    public void playOutWorldlySounds(List<Vector3> positions,bool inside)
    {
        Debug.Log(inside);
        if (hasChanged(inside))
        {
            playThePokomoSinging();
        }

        //coroutine = playMusic(5);
       // StartCoroutine(coroutine);
    }


    bool lastChecked = false;
    bool hasChanged(bool inside)
    {

        if (inside!= lastChecked)
        {
            Debug.Log("has changed");
            lastChecked = inside;
            return true;
        }
        else
        {
            lastChecked = inside;
            return false;
        }
    }

    public void playTheRootsGrowSounds(Transform treePos)
    {
        //create an instance of soundObject that is public handed in in the inspector
        //that contains the Audiosource at the position handed in in the argument
  
        GameObject treeSoundObject = Instantiate(soundObject, treePos.position, Quaternion.identity);

        
        AudioSource treeSoundAudioSource = treeSoundObject.GetComponent<AudioSource>();

        AudioClip treeSound= treeSoundAudioSource.clip = soundsArray[(int)sounds.treeSound] ;
        treeSoundAudioSource.Play();
        Debug.Log("pokomo song");
        Destroy(treeSoundObject, treeSound.length);
    }
}
