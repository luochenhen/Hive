using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform tr;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Red")
        //    Destroy(this);


    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
}
