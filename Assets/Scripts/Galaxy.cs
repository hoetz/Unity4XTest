using UnityEngine;

public class Galaxy : MonoBehaviour
{
    public int numberOfStars = 300;
    public int maximumRadius = 100;
    public float minDistBetweenStars;
    public int minRadius = 0;

    // Use this for initialization
    private void Start()
    {
        SanityChecks();

        int failCount = 0;

        for (int i = 0; i < numberOfStars; i++)
        {
            Star starData = new Star("Star" + i, Random.Range(1, 10));

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