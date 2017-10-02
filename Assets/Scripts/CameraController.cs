using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Transform rotationObject;
    Transform zoomObject;

    public float panSpeed = 100;

    void Awake()
    {
        rotationObject = transform.GetChild(0);
        zoomObject = rotationObject.transform.GetChild(0);
        ResetCamera();
    }

    void ChangePosition()
    {
        if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
        {
            float distance = panSpeed * Time.deltaTime;
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            float dampingFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

            transform.Translate(distance * dampingFactor*direction);
        }
    }

    public void ResetCamera()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        ChangePosition();
    }
}
