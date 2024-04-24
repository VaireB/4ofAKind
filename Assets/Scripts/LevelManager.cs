using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelManager : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load

    public void CompleteLevel()
    {
        // Check if the next scene name is assigned
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Load the next scene by name
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not assigned!");
        }
    }
}

#if UNITY_EDITOR
// Custom property drawer for nextSceneName
[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Cast target to LevelManager
        LevelManager manager = (LevelManager)target;

        // Display a warning if nextSceneName is empty
        if (string.IsNullOrEmpty(manager.nextSceneName))
        {
            EditorGUILayout.HelpBox("Assign the next scene name to load.", MessageType.Warning);
        }
    }
}
#endif
