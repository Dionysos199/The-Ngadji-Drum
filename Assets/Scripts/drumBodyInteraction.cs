using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

//Attach this script to any GameObject in your scene to spawn a cube and change the material color
[System.Serializable]
public class  InsideInfluenceArea:UnityEvent<List<Vector3>,bool>
{

  
}

public class drumBodyInteraction : MonoBehaviour
{
  
    [SerializeField] private float threshold;
    [SerializeField] InsideInfluenceArea insideInfluenceArea;
    List<InputDevice> bothControllers;

    [SerializeField] Transform leftHand;

    [SerializeField] Transform rightHand;
    bool inside;

    List<Vector3> positions;
    private void Start()
    {

    }
   
    private void Update()
    {
            
            // Debug.Log("orientation " + transform.forward);
            Vector3 leftHandPos = leftHand.position;

            Vector3 rightHandPos = rightHand.position;

            float distToRHand = Vector3.Distance(rightHandPos, transform.position);
            float distToLHand = Vector3.Distance(leftHandPos, transform.position);

            // Debug.Log("distance to left Hand: "+distToLHand);
            // Debug.Log("distance to right Hand: "+distToRHand);
            if (distToLHand < threshold || distToRHand < threshold)
            {
                inside = true;

            }
            else
            {
                inside = false;
            }
            positions = new List<Vector3> { leftHandPos, rightHandPos };

   

        if (inside==true)
        {
            insideInfluenceArea.Invoke(positions, inside);


        }
       
    }

}
