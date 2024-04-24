using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private int totalCoins = 4; // Total number of coins in the level
    private int collectedCoins = 0; // Number of coins collected
    private bool allCoinsCollected = false; // Flag to track if all coins have been collected

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = "Collected: " + collectedCoins.ToString() + "/" + totalCoins.ToString() + " coins";

        // Check if all coins have been collected
        if (collectedCoins >= totalCoins)
        {
            allCoinsCollected = true;
        }
    }

    public void IncrementCollectedCoins()
    {
        collectedCoins++;
        UpdateUI();
    }

    public bool AllCoinsCollected()
    {
        return allCoinsCollected;
    }
}
