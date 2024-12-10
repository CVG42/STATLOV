using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField] bool isFlipped;

    Rigidbody2D rb;
    Vector2 movementInput;
    Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = movementInput * moveSpeed;
        LookPosition();
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
            animator.SetBool("isWalking", false);

        if (!isFlipped)
            Flip(movementInput.x);

        movementInput = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    private void LookPosition()
    {
        if (isFlipped)
            Flip(movementInput.x);
    }

    private void Flip(float x)
    {
        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
    }
}
