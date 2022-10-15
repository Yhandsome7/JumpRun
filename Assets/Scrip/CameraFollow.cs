using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject character;
    
    // Start is called before the first frame update    
    // Update is called once per frame
    void Update()
    {   
        transform.position =  character.transform.position + new Vector3(0,0,-10);
    }
}
