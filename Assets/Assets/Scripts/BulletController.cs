using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRGB2D;
    [SerializeField] private float velocityMultiplier;
    [SerializeField] private int damage;
    [SerializeField] private AudioClip shootSound; // AudioClip para el sonido de disparo
    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido

    public event Action<int, HealthBarController> onCollision;

    private void Awake()
    {
        // Obtener o añadir un componente AudioSource al objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void SetUpVelocity(Vector2 velocity, string newTag)
    {
        myRGB2D.velocity = velocity * velocityMultiplier;
        gameObject.tag = newTag;

        DamageManager.instance.SubscribeFunction(this);

        // Reproducir el sonido de disparo
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(gameObject.tag) && (other.CompareTag("Player") || other.CompareTag("Enemy")))
        {
            if (other.GetComponent<HealthBarController>())
            {
                onCollision?.Invoke(damage, other.GetComponent<HealthBarController>());
            }
            Destroy(this.gameObject);
        }
    }
}
