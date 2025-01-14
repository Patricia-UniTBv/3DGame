using UnityEngine;
using UnityEngine.Audio;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;
    public AudioClip coinSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Audio Source not found!");
        }
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(coinSound);
        Destroy(gameObject);
    }
}
