using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour {

    public static SolarSystem SolarSystemInstance;
    public Button galaxyViewButton;

    void OnEnable()
    {
        SolarSystemInstance = this;
        galaxyViewButton.interactable = false;
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame

    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
        {
            Star star = Galaxy.GalaxyInstance.ReturnStarFromGameObject(hit.transform.gameObject);
            Debug.Log(string.Format("You clicked on {0} with {1} planets", star.starName, star.numberOfPlanets));
            Galaxy.GalaxyInstance.DestroyGalaxy();

            CreateSolarSystem(star);
        }
    }

    void CreateSolarSystem(Star star)
    {
        Random.InitState(Galaxy.GalaxyInstance.seedNumber);

        SpaceObjects.CreateSphereObject(star.starName, Vector3.zero, this.transform);

        for (int i = 0; i < star.planetList.Count; i++)
        {
            Planet planet = star.planetList[i];

            float distance = (i + 1) * 5;
            float angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 planetPos = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

            SpaceObjects.CreateSphereObject(planet.planetName, planetPos, this.transform);

        }

        galaxyViewButton.interactable = true;
    }

    public void DestroySolarSystem()
    {
        while (transform.childCount>0)
        {
            Transform go = transform.GetChild(0);
            go.SetParent(null);
            Destroy(go.gameObject);
        }
        galaxyViewButton.interactable = false;

    }
}
