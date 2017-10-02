using UnityEngine;

public class Galaxy : MonoBehaviour
{
    public int numberOfStars = 300;
    public int maximumRadius = 100;
    public float minDistBetweenStars;
    public int minRadius = 0;
    public string[] availablePlanetTypes = { "Barren", "Terran", "Gas Giant" };

    // Use this for initialization
    private void Start()
    {
        SanityChecks();

        int failCount = 0;

        for (int i = 0; i < numberOfStars; i++)
        {
            Star starData = new Star("Star" + i, Random.Range(1, 10));
            CreatePlanetData(starData);

            Vector3 cartPosition = RandomPosition();

            Collider[] positionCollider = Physics.OverlapSphere(cartPosition, minDistBetweenStars);

            if (positionCollider.Length == 0)
            {
                CreateSphereGameObject(starData, cartPosition);

                failCount = 0;
            }
            else
            {
                i--;
                failCount++;
            }
            if (failCount > numberOfStars)
            {
                Debug.LogError("Could not fit all the stars");
                break;
            }
        }

    }

    void CreatePlanetData(Star starData)
    {
        for (int i = 0; i < starData.numberOfPlanets; i++)
        {
            string name = starData.starName + (starData.planetList.Count + 1).ToString();

            int random = Random.Range(1, 100);
            string type = "";

            if (random<40)
            {
                type = availablePlanetTypes[0];
            }
            else if (random>=40 && random <50)
            {
                type = availablePlanetTypes[1];
            }
            else
            {
                type = availablePlanetTypes[2];
            }

            Planet planetData = new Planet(name, type);
            Debug.Log("Planet " + name+" type: "+type);

            starData.planetList.Add(planetData);
        }
    }

    private static void CreateSphereGameObject(Star starData, Vector3 cartPosition)
    {
        var starGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        starGO.name = starData.starName;
        starGO.transform.position = cartPosition;
    }

    private Vector3 RandomPosition()
    {
        float distance = Random.Range(minRadius, maximumRadius);
        float angle = Random.Range(0, 2 * Mathf.PI);

        Vector3 cartPosition = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));
        return cartPosition;
    }

    private void SanityChecks()
    {
        if (minRadius > maximumRadius)
        {
            int temp = maximumRadius;
            maximumRadius = minRadius;
            minRadius = temp;
        }
    }
}