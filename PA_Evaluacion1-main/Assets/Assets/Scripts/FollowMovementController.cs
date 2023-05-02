using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovementController : MonoBehaviour
{
    [SerializeField] private Transform ogreTransform;
    [SerializeField] private Rigidbody2D ogreRB2D;
    [SerializeField] private float velocityModifier;
    [SerializeField] private BulletController bullet;
    private Transform currentTarget;
    private bool isFollowing;
    private bool isMoving;
    private bool canShoot = true;

    private void Start() {
        currentTarget = transform;
    }

    private void Update() {
        if(isMoving){
            ogreRB2D.velocity = (currentTarget.position - ogreTransform.position).normalized * velocityModifier;

            if(isFollowing && canShoot){
                StartCoroutine(ShootBullet());
                canShoot = false;
            }

            CalculateDistance();
        }else{
            ogreRB2D.velocity = (currentTarget.position - ogreTransform.position).normalized * velocityModifier;
            CalculateDistance();
        }
    }

    private void CalculateDistance(){
        if((currentTarget.position - ogreTransform.position).magnitude < 0.05f){
            ogreTransform.position = currentTarget.position;
            isMoving = false;
            ogreRB2D.velocity = Vector2.zero;
        }else{
            isMoving = true;
        }
    }
    
    IEnumerator ShootBullet(){
        Instantiate(bullet, ogreTransform.position, Quaternion.identity).SetUpVelocity(ogreRB2D.velocity, "Enemy");
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            currentTarget = other.transform;
            isFollowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            currentTarget = transform;
            isFollowing = false;
        }
    }
}
