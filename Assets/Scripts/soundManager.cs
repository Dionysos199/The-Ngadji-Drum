using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class soundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public  GameObject soundObject;
    public  AudioClip[] soundsArray;
      AudioSource drumSound;
    public  Transform hideCenter;

    public WallsReaction wallReaction;

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
    public enum sounds {
        JAPENESEDRUMSOUND, LionsRoar, PokomoSinging, treeSound, nearDrumHandsEffect, CRACKINGSOUND,
        CRUMBLEDOWN, NEONLIGHTON,ELECTRICBUZZ
    }

    public  void playDrumSound( float hitForce, Vector3 hitPos,int a)
    {

        GameObject drumSoundObject = Instantiate(soundObject, hitPos, Quaternion.identity);
        drumSound = drumSoundObject.GetComponent<AudioSource>();
        
     

        drumSound.clip = soundsArray[(int)sounds.JAPENESEDRUMSOUND];
        //Debug.Log("now Playing"+drumSound.clip);

        //for getting the pitch we need the distance from the center of the hide
        // of the hit, the further it is from the center the higher the pitch is 
        float distFromCenter = Vector3.Distance(hitPos, hideCenter.position);
        drumSound.pitch = distFromCenter*3;

        //Volume of the sound is proportionnal the speed of the hit
        drumSound.volume = hitForce/10;
        drumSound.Play();

        wallReaction.drumhitSound = drumSound;


        Destroy(drumSoundObject, drumSound.clip.length);
    }
    public  void playElectricWaveEffect(float intensity,Vector3 position)
    {
        GameObject handsNearSoundEffectObject = Instantiate(soundObject, position, Quaternion.identity);
        AudioSource electricEffectAS=handsNearSoundEffectObject.GetComponent<AudioSource>();
        electricEffectAS.clip = soundsArray[(int)sounds.nearDrumHandsEffect];
        electricEffectAS.Play();
        electricEffectAS.volume = intensity;
        Destroy(handsNearSoundEffectObject, 3);

    }


    public void instantiateSound( Vector3 position, AudioClip clip,float destroyAfter)
    {
        GameObject _soundObject = Instantiate(soundObject, position, Quaternion.identity);
        AudioSource attachedAudio = _soundObject.GetComponent<AudioSource>();
        attachedAudio.clip = clip;
        attachedAudio.Play();
        Destroy(_soundObject, destroyAfter);

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
