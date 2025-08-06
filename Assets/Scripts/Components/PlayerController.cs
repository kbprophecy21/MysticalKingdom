using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    protected CharacterController controller;
    protected Vector3 moveDirection;


    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        HandleMovement();
    }
    protected virtual void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction.magnitude > 0.1f)
        {
            moveDirection.Normalize();
        }
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}
