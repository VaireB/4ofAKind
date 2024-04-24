using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalArea : MonoBehaviour
{
    private bool goalReached = false; // Flag to track if the goal has been reached

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinUI coinUI = FindObjectOfType<CoinUI>();

            // Check if all coins have been collected
            if (coinUI != null && coinUI.AllCoinsCollected())
            {
                Debug.Log("All coins collected! Proceeding to the next level.");
                goalReached = true; // Set the flag to indicate goal reached

                // Check if the goal has been reached and all coins have been collected
                if (goalReached)
                {
                    Debug.Log("Level completed! Proceeding to the next level.");
                    LoadNextScene();
                }
            }
            else
            {
                Debug.Log("Collect all coins first!");
            }
        }
    }

    private void LoadNextScene()
    {
        // Load the next scene if available
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No next scene available. End of level progression.");
            // Implement logic for end of level progression here
        }
    }
}
