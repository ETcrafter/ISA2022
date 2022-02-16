using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLooksAtCamera : MonoBehaviour
{
    public GameObject Target;
   
    void Start()
    {
        Target = GameObject.Find("Main Camera");
    }

   
    void Update()
    {
       
    }
}
