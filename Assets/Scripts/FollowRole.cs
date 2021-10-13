using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRole : MonoBehaviour
{
    private GameObject role;
    private Transform roleTransform;

    // Start is called before the first frame update
    void Start()
    {
        role = GameObject.FindGameObjectWithTag("Player");
        roleTransform = role.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rolePos = roleTransform.position;
        //print("role:" + rolePos.y);
        //print("Camera:" + this.transform.position.y);
        if (rolePos.y < this.transform.position.y) {
            //print("DOWN!!");
            this.transform.position = new Vector3(this.transform.position.x, rolePos.y, this.transform.position.z);
        }
    }
}
