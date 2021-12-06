using UnityEngine;
using UnityEngine.SceneManagement;

public class Collider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartWinSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void DisableMovement()
    {
        GetComponent<RocketMovement>().enabled = false;
    }

    void StartWinSequence()
    {
        DisableMovement();
        Invoke("LoadNextLevel", 1f);
    }

    void StartCrashSequence()
    {
        DisableMovement();
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
