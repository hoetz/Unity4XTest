using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Transform rotationObject;
    Transform zoomObject;

    public float zoomedInAngle = 45;
    public float zoomedOutAngle = 90;
    public float panSpeed = 100;
    public float minZoom = 20;
    public float maxZoom = 200;
    float zoomLevel = 0;
    public bool inverseZoom;

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

            ClampCameraPan();
        }
    }

    void ClampCameraPan()
    {
        Vector3 pos = this.transform.position;

        if (Galaxy.GalaxyInstance.galaxyView==true)
        {
            pos.x = Mathf.Clamp(transform.position.x, -Galaxy.GalaxyInstance.maximumRadius, Galaxy.GalaxyInstance.maximumRadius);
            pos.z = Mathf.Clamp(transform.position.z, -Galaxy.GalaxyInstance.maximumRadius, Galaxy.GalaxyInstance.maximumRadius);
        }
        else
        {
            pos.x = Mathf.Clamp(transform.position.x, -(Galaxy.maxNumberOfPlanets*5), Galaxy.maxNumberOfPlanets*5);
            pos.z = Mathf.Clamp(transform.position.z, -(Galaxy.maxNumberOfPlanets * 5), Galaxy.maxNumberOfPlanets * 5);
        }
        this.transform.position = pos;
    }

    void ChangeZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            if (inverseZoom==false)
            {
                zoomLevel = Mathf.Clamp01(zoomLevel - Input.GetAxis("Mouse ScrollWheel"));
            }
            else
            {
                zoomLevel = Mathf.Clamp01(zoomLevel + Input.GetAxis("Mouse ScrollWheel"));
            }

            float zoom = Mathf.Lerp(-minZoom, -maxZoom, zoomLevel);
            zoomObject.transform.localPosition = new Vector3(0, 0, zoom);
        }
    }

    public void ResetCamera()
    {
        this.transform.position = new Vector3(0, 0, 0);
        zoomLevel = 0;
        rotationObject.transform.rotation = Quaternion.Euler(zoomedInAngle, 0, 0);
        zoomObject.transform.localPosition = new Vector3(0, 0, minZoom);
    }
	
	// Update is called once per frame
	void Update () {
        ChangePosition();
        ChangeZoom();
    }
}
