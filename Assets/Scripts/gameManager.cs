using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static gameManager Instance { get; private set; }
    public  string[] Phases { get; private set; }
    public  string Phase { get;  set; }
    public void setPhase (float a, Vector3 v, int numberOfHits)
    {
        Phase = Phases[numberOfHits];
        Debug.Log(Phase);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        Phases = new string[] { "Phase1", "Phase2", "Phase3", "Phase4", "Phase5" };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
