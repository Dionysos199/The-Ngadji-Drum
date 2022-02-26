using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public Transform hideCenter;
    public static gameManager Instance { get; private set; }
    public  string[] Phases { get; private set; }
    public  string Phase { get;  set; }

    public int numberOfHits;
    public void setPhase (float a, Vector3 v, int numberOfHits)
    {
        Phase = Phases[numberOfHits];
        Debug.Log(Phase);
    }

// Making this class a singleton
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
}
