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
        int failCount = 0;

        if (minRadius>maximumRadius)
        {
            int temp = maximumRadius;
            maximumRadius = minRadius;
            minRadius = temp;
        }

        for (int i = 0; i < numberOfStars; i++)
        {
            Star starData = new Star("Star" + i, Random.Range(1, 10));

            float distance = Random.Range(minRadius, maximumRadius);
            float angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 cartPosition = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

            Collider[] positionCollider = Physics.OverlapSphere(cartPosition, minDistBetweenStars);

            if (positionCollider.Length == 0)
            {
                var starGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                starGO.name = starData.starName;
                starGO.transform.position = cartPosition;
                failCount = 0;
            }
            else
            {
                i--;
                failCount++;
            }
            if (failCount>numberOfStars)
            {
                Debug.LogError("Could not fit all the stars");
                break;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}