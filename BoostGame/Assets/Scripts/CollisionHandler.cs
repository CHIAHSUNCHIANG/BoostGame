using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip end;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem endParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; } // if isTransitioning is true 如果正在轉換場景，就不做以下動作 

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;            
            default:
                StartCrashSequence();               
                break;
        }
    }
    void StartSuccessSequence()
    {        
        isTransitioning = true;
        audioSource.Stop(); // turn off all the sound before the success sound start
        audioSource.PlayOneShot(success);
        successParticles.Play();
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); // turn off all the sound before the success sound start
        audioSource.PlayOneShot(end);
        endParticles.Play();
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        //<GetComponent<Scripts>().enalbe = boolean
        Invoke("ReloadLevel", levelLoadDelay);
        //<Invoke("MethodName", DelayInSeconds)
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    
}
