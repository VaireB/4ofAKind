using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 180f; // Adjust the rotation speed as needed

    void Start()
    {
        // You can optionally set a random initial rotation for variation
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");
            Destroy(gameObject);

            // Send a message to the CoinUI script to update the coin count
            GameObject.FindObjectOfType<CoinUI>()?.IncrementCollectedCoins();
        }
    }

    void Update()
    {
        // Rotate the coin continuously
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
