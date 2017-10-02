using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

    public static SolarSystem SolarSystemInstance;

    void OnEnable()
    {
        SolarSystemInstance = this;
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
        GameObject starGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        starGO.transform.position = Vector3.zero;
        starGO.name = star.starName;
        starGO.transform.SetParent(this.transform);

        for (int i = 0; i < star.planetList.Count; i++)
        {
            Planet planet = star.planetList[i];

            float distance = (i + 1) * 5;
            float angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 planetPos = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

            SpaceObjects.CreateSphereObject(planet.planetName, planetPos, this.transform);

        }
    }
}
