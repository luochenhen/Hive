using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grey : MonoBehaviour
{
    private int destoryCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player") {
            destoryCount--;
            print(this.name + ":" + destoryCount);
            if (destoryCount <= 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
