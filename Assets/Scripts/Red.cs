using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
    private int destoryCount = 0;
    private string[] destoryName = new string[4];
    private string selfTag;
    // Start is called before the first frame update
    void Start()
    {
        // Hard Code for test
        if (this.name == "Red") 
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        if (this.name == "Red (1)")
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
        Debug.Log(collision.collider.tag == "Player");
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
        /*if (collision.collider.tag == selfTag)
        {
            //print("RED!!");
            FindTarget();
            //print(this.name + " : " + this.destoryCount);
            //this.destoryName[this.destoryCount] = collision.collider.name;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
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
        }*/
        if (collision.collider.tag == "Platform") {
            Destroy(this.gameObject);
        }
        else
        {
            FindTarget();
            //print(this.name + " : " + this.destoryCount);
            //this.destoryName[this.destoryCount] = collision.collider.name;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Rigidbody Rb;
            if (collision.collider.TryGetComponent<Rigidbody>(out Rb))
            {
                Rb.velocity = new Vector3(0, 0, 0);
            }
            //print(this.name + "--->" + collision.collider.name);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //print(this.name + "<---" + collision.collider.name);
    }

    public void AddCount() 
    {
        this.destoryCount++;
    }

    public void ReduceCount() 
    {
        this.destoryCount--;
    }

    private void FindTarget() {
        bool isDestory = false;
        float dis = this.gameObject.GetComponent<Renderer>().bounds.size.x;
        dis = dis * 1.5f;
        //print(this.name + ":" + this.transform.position.x+"||"+dis);
        Vector3 pos = this.transform.position;
        //对移动后的物块上方进行三消
        isDestory = DestoryTarget(isDestory, pos, this.transform.up, dis);
        // 对移动后的物块下方进行三消
        isDestory = DestoryTarget(isDestory, pos, -this.transform.up, dis);
        // 对移动后的物块右方进行三消
        isDestory = DestoryTarget(isDestory, pos, this.transform.right, dis);
        // 对移动后的物块左方进行三消
        isDestory = DestoryTarget(isDestory, pos, -this.transform.right, dis);
        if (isDestory) {
            Destroy(this.gameObject);
        }
    }

    private bool DestoryTarget(bool isDestory,Vector3 pos,Vector3 dir,float dis) {
        RaycastHit[] hits = Physics.RaycastAll(pos, dir, dis * 1.5f);
        int destoryCount = 0;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == selfTag) destoryCount++;
            //print(this.name + "---" + hits[i].collider.name);
        }
        if (destoryCount >= 2)
        {
            isDestory = true;
            for (int i = 0; i < hits.Length; i++)
            {
                Destroy(GameObject.Find(hits[i].collider.name));
            }
        }
        return isDestory;
    }
}
