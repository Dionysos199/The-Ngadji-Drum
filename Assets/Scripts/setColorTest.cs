using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setColorTest : MonoBehaviour
{

    [SerializeField] private Material myMaterial;
    void Start()
    {
        //Create a new cube primitive to set the color on

        //Get the Renderer component from the new cube

        //Call SetColor using the shader property name "_Color" and setting the color to red
        myMaterial.color = Color.green;
    }
}