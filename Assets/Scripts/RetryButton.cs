using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    void Start()
    {
        // Add listener for button click event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnRetryButtonClick);
        }
        else
        {
            Debug.LogError("RetryButton script attached to a GameObject without a Button component.");
        }
    }

    void OnRetryButtonClick()
    {
        RetryLevel();
    }

    public void RetryLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
