using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Barrier : MonoBehaviour
{
    public int startSize = 3;
    public int minSize = 1;
    public int maxSize = 6;

    public float speed = 2.0f;

    private Vector3 targetScale;
    private Vector3 baseScale;
    private int currScale;

    // Material
    [SerializeField] private Material m_Material;

    [SerializeField] float emissionIntensity = 10.0f;

    bool triggerInput = true;
    bool triggerInput2 = false;


    void Start()
    {
        baseScale = transform.localScale;
        transform.localScale = baseScale * startSize;
        currScale = startSize;
        targetScale = baseScale * startSize;

        m_Material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);

        float scale_mag = transform.localScale.magnitude / 500;

        // If you don't want an eased scaling, replace the above line with the following line
        //   and change speed to suit:
        // transform.localScale = Vector3.MoveTowards (transform.localScale, targetScale, speed * Time.deltaTime);

        if (triggerInput) //triggerInput is the player continously playing the drum
        {
            ChangeSize(true);
            Material(true, scale_mag);

        }

        else // if the player is no longer playing, the sphere starts becoming smaller after a certain amount of time, hiding the landscape again
        {

            ChangeSize(false);
        }
        
    }

    public void ChangeSize(bool bigger)
    {
        //first trigger for the sphere to grow while the player is playing
        if (bigger)
            currScale++;
        else
            currScale--;

        currScale = Mathf.Clamp(currScale, minSize, maxSize + 1);

        targetScale = baseScale * currScale;
    }

    public void Material(bool color, float scale_mag)
    {
        // first trigger for the emission to stop when the sphere has gotten to its maximum size and the building is destroyed
        
        

            Color mycolor = m_Material.color;
            m_Material.color = Color.blue;

            //mycolor.a = 0;

            //m_Material.EnableKeyword("_EMISSION");
            m_Material.SetColor("_EmissionColor", mycolor * scale_mag);

            //m_Material.DisableKeyword("_EMISSION");
        
        // second trigger for the end of the scene when the emission flashes and the player ends up at the beginning
        

    }
}
