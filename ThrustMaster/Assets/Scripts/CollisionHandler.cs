using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    
    bool isTransitioning = false;
    bool disableCollisions = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {   
        // Cheats key for debugging/testing. 
        // Press L to skip to next level or C to disable collisions
        if (Input.GetKeyDown(KeyCode.L)) {StartNextLevel();}
        if (Input.GetKeyDown(KeyCode.C)) {disableCollisions = !disableCollisions;} // Toggle
    }

    void OnCollisionEnter(Collision other) {
        // Stops collision events after one has happened
        if (disableCollisions) {return;}
        if (isTransitioning || disableCollisions) {return;}

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
        crashParticles.Play();
        // Disables player movement after crash
        GetComponent<Movement>().enabled = false;
        // Invoke adds delay
        Invoke("ReloadLevel", delay);
    }

    void StartNextLevel() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
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
