using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    [SerializeField]
    float offsetY, offsetZ;

    // Update is called once per frame
    void Update()
    {
        //Set Transform of the camera same as transform of the target with y and z offset
        if(Target != null)
        {
            transform.position = new Vector3(Target.position.x, Target.position.y + offsetY, Target.position.z - offsetZ);
        }
        

        
    }
}
