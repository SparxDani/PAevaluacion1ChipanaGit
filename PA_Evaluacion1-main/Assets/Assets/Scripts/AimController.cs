using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [Header("Vector 3 Positions")]
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 mousePositionCamera;
    [SerializeField] private Vector3 mousePositionCameraNormalized;
    [SerializeField] private Vector3 mousePositionPlayer;
    [Header("Targets")]
    [SerializeField] private Transform mouseTarget;
    [SerializeField] private Transform mouseTargetCamera;
    [SerializeField] private Transform mouseTargetCameraNormalized;
    [SerializeField] private Transform mouseTargetPlayer;

    float rayDistance = 5f;

    private void Update() {
        mousePosition = Input.mousePosition;
        mouseTarget.position = mousePosition;
        Debug.DrawRay(transform.position, mousePosition * rayDistance, Color.gray);
        
        mousePositionCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseTargetCamera.position = mousePositionCamera;
        Debug.DrawRay(transform.position, mousePositionCamera * rayDistance, Color.green);

        mousePositionCameraNormalized = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        mouseTargetCameraNormalized.position = mousePositionCameraNormalized;
        Debug.DrawRay(transform.position, mousePositionCameraNormalized * rayDistance, Color.yellow);

        Vector3 distance = mousePositionCamera - transform.position;
        mousePositionPlayer = distance;
        mouseTargetPlayer.position = mousePositionPlayer;
        Debug.DrawRay(transform.position, mousePositionPlayer * rayDistance, Color.red);
    }
}
