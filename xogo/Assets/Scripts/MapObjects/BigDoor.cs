using UnityEngine;

public class BigDoor : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip openingSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource GetAudioSource(){
        return audioSource;
    }
    public AudioClip GetOpeningSound(){
        return openingSound;
    }
}
