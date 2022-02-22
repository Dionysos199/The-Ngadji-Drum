using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightManager : MonoBehaviour
{
    public GameObject[] lightObjects;
    public drumInteraction drumInteraction;
    WallsReaction wallreaction;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        /*foreach (var lightObject in lightObjects)
        {
            FlickeringLight_ flickeringLight_ = GetComponent<FlickeringLight_>();

            drumInteraction.hitDrum.AddListener(flickeringLight_.flicker);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
