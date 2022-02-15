using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideManger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.Phase == "Phase5")
        {
            gameObject.SetActive(true);
        }
    }
}
