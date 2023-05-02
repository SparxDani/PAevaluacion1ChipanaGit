using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private CameraController cameraReference;

    private void Start() {
        GetComponent<HealthBarController>().onHit += cameraReference.CallScreenShake;
    }

    private void Update() {
        Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector3 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);
    
        Vector3 distance = mouseInput - transform.position;
        Debug.DrawRay(transform.position, distance * rayDistance, Color.red);

        if(Input.GetMouseButtonDown(0)){
            BulletController myBullet =  Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            myBullet.SetUpVelocity(distance.normalized, gameObject.tag);
        }else if(Input.GetMouseButtonDown(1)){
            Debug.Log("Left Click");
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
