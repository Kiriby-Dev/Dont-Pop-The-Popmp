using UnityEngine;

public class Sonido : MonoBehaviour
{

    private AudioSource audioSource;
    audioSource = GetComponent<AudioSource>();
    audioSource.Play();
    
}
