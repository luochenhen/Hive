using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : MonoBehaviour
{
    private int destoryCount = 0;
    private string[] destoryName = new string[2];
    private string selfTag;
    // Start is called before the first frame update
    void Start()
    {
        // Hard Code for test
        if (this.name == "Green")
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        if (this.name == "Green (2)")
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        selfTag = this.tag;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Vector3 direction = collision.collider.GetComponent<Rigidbody>().velocity.normalized;
            if (direction.y < direction.x && direction.y > -direction.x)  //right
                this.GetComponent<Rigidbody>().velocity = new Vector3(10, 0, 0);
            else if (direction.y > direction.x && direction.y > -direction.x)  //up
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
            else if (direction.y < -direction.x && direction.y > direction.x)  //left
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, -10, 0);
            else if (direction.y < direction.x && direction.y < -direction.x) //down
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, -10, 0);
        }
        if (collision.collider.tag == selfTag)
        {
            //print("RED!!");
            print(this.name + " : " + this.destoryCount);
            this.destoryName[this.destoryCount] = collision.collider.name;
            collision.collider.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.AddCount();
            //print(this.name + " : " + this.destoryCount);
            if (this.destoryCount >= 2)
            {
                GameObject object1 = GameObject.Find(this.destoryName[0]);
                GameObject object2 = GameObject.Find(this.destoryName[1]);
                Destroy(object1);
                Destroy(object2);
                Destroy(this.gameObject);
            }
        }
        else
        {
            print(this.name + "--->" + collision.collider.name);
            Rigidbody Rb;
            if (collision.collider.TryGetComponent<Rigidbody>(out Rb))
            {
                Rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public void AddCount()
    {
        this.destoryCount++;
    }

    public void ReduceCount()
    {
        this.destoryCount--;
    }


}
