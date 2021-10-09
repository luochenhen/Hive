using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    public float timer = 0;
    public int frameNumber = 10;
    public int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f / frameNumber) {
            frameCount++;
            timer -= 1.0f / frameNumber;
            int frameIndex = frameCount % 3;

            this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.3333f * frameIndex, 0));
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector3 vel = this.GetComponent<Rigidbody>().velocity;
            this.GetComponent<Rigidbody>().velocity = new Vector3(1,5,0);
        }


    }
}
