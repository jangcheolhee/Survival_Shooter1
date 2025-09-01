using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Animator animator;
    Rigidbody rb;
    public float speed;
    public float rotateSpeed = 180f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        transform.LookAt(playerInput.Rotate);

        Vector3 move = new Vector3(playerInput.MoveH, 0f, playerInput.MoveV) * speed * Time.deltaTime ;
        rb.MovePosition(transform.position + move);

        animator.SetFloat("Move",Mathf.Abs(playerInput.MoveH)+ Mathf.Abs(playerInput.MoveV));
    }
   

}
