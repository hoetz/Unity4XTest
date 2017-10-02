using UnityEngine;

public class Galaxy : MonoBehaviour
{
    public int numberOfStars = 300;
    public int maximumRadius = 100;
    public float minDistBetweenStars;
    // Use this for initialization
    private void Start()
    {
        int failCount = 0;
        for (int i = 0; i < numberOfStars; i++)
        {
            float distance = Random.Range(0, maximumRadius);
            float angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 cartPosition = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

            Collider[] positionCollider = Physics.OverlapSphere(cartPosition, minDistBetweenStars);

            if (positionCollider.Length == 0)
            {
                var starGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
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