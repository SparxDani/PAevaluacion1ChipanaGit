using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private CameraController cameraReference;
    public PlayerInput _playerInput;

    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido

    private void Awake()
    {
        // Obtener o a�adir un componente AudioSource al objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        HealthBarController healthBarController = GetComponent<HealthBarController>();
        healthBarController.onHit += OnHit;
        healthBarController.onHit += cameraReference.CallScreenShake;
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnHit()
    {
        // Reproducir el sonido de golpe
        // Aqu� debes asignar el AudioClip adecuado para el sonido de golpe
        // Puedes asignarlo desde el Inspector de Unity
        AudioClip hitSound = null; // Asigna el AudioClip para el sonido de golpe
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void Update()
    {
        Vector2 movementInput = _playerInput.actions["Movement"].ReadValue<Vector2>();
        myRBD2.velocity = movementInput * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 aimInput = _playerInput.actions["Aim"].ReadValue<Vector2>();
        Vector3 mouseInput = Camera.main.ScreenToWorldPoint(aimInput);

        CheckFlip(mouseInput.x);

        Vector3 distance = mouseInput - transform.position;
        Debug.DrawRay(transform.position, distance * rayDistance, Color.red);

        if (_playerInput.actions["Fire"].triggered)
        {
            BulletController myBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            myBullet.SetUpVelocity(distance.normalized, gameObject.tag);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Click");
        }
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
