using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int currentLane = 1;
    public float speed = 1f;

    public bool isMoving = false;
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private SpriteRenderer handRenderer;
    [SerializeField] private Transform handTransform; 
    [SerializeField] private Vector3 handOffsetUp;  
    [SerializeField] private Vector3 handOffsetDown; 
    [SerializeField] private Vector3 handOffsetIdle;
    private Vector3 targetPosition;
    void Start()
    {
        targetPosition = LevelManager.instance.playerPoints[currentLane].position;
        transform.position = targetPosition;
    }

    void Update()
    {
        if (PauseScript.instance.isPaused) {
            return;
        }
        HandleInput();
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isMoving = false;
                StopMoveAnimation();
            }
        }
        
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isMoving)
        {
            if (currentLane > 0) {
                currentLane--;
                bodyAnimator.SetBool("isRunningUp", true);
                handRenderer.enabled = false;
                MoveToLane();
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && !isMoving)
        {
            if (currentLane < 2) {
                currentLane++;
                bodyAnimator.SetBool("isRunningDown", true);
                handAnimator.SetBool("isRunningDown", true);
                handTransform.localPosition = handOffsetDown;
                MoveToLane();
            }
        }
    }

    void MoveToLane() {
        targetPosition = LevelManager.instance.playerPoints[currentLane].position;
        isMoving = true;
    }

    void StopMoveAnimation()
    {
        bodyAnimator.SetBool("isRunningUp", false);
        bodyAnimator.SetBool("isRunningDown", false);
        handAnimator.SetBool("isRunningDown", false);

        handTransform.localPosition = handOffsetIdle;
        handRenderer.enabled = true;
    }
}
