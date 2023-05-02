using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float raycastDistance = 5f;
    [SerializeField] private LayerMask layerInteraction;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private float fastVelocity = 0f;
    private float normalVelocity;

    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;

        normalVelocity = velocityModifier;
        fastVelocity = velocityModifier * 2.5f;
    }

    private void Update() {
        CheckNewPoint();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }

    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            transform.position = currentPositionTarget.position;
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            CheckFlip(myRBD2.velocity.x);
        }
        Vector2 distanceTarget = currentPositionTarget.position - transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, distanceTarget, raycastDistance, layerInteraction);
        if(hit2D){
            if(hit2D.collider.CompareTag("Player")){
                velocityModifier = fastVelocity;
            }
        }else{
            velocityModifier = normalVelocity;
        }

        myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
        Debug.DrawRay(transform.position, distanceTarget * raycastDistance, Color.cyan);
        
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
