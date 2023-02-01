using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    AudioSource audioSource;
    bool isTransitioning = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other) {
        // Stops collision events after one has happened
        if (isTransitioning) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;

            case "Finish":
                StartNextLevel();
                break;

            default:
                // Teleports player to the spawnpoint when obstacle is hit
                StartCrash();
                break;
        }

    }

    void StartCrash() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        // Disables player movement after crash
        GetComponent<Movement>().enabled = false;
        // Invoke adds delay
        Invoke("ReloadLevel", delay);
    }

    void StartNextLevel() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }
    void ReloadLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    void LoadNextLevel()
    {   
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex+1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
}
