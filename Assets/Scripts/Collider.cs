using UnityEngine;
using UnityEngine.SceneManagement;

public class Collider : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;
    AudioSource audiosource;

    bool isTransitioning = false;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.Stop();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

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
        isTransitioning = true;
        DisableMovement();
        audiosource.Stop();
        audiosource.PlayOneShot(winSound);
        Invoke("LoadNextLevel", 1f);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        DisableMovement();
        audiosource.Stop();
        audiosource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
        isTransitioning = false;
    }
}
