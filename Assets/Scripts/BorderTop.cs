using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTop : MonoBehaviour
{
    private GameObject cam;
    private Transform cameraTransform;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        height = cam.GetComponent<Camera>().orthographicSize;
        cameraTransform = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = cameraTransform.position;
        this.transform.position = new Vector3(this.transform.position.x, camPos.y + height, this.transform.position.z);
    }
}
