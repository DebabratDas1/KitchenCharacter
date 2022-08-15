using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;


    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private PlayerAnim anim = PlayerAnim.Idle;

    private void Start()
    {
        //
        animator.SetBool("WillWalk", true);


    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0f)
        {
            anim = PlayerAnim.Walk;
            //animator.SetInteger("Walk", 1);
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
            anim = PlayerAnim.Idle;
        PlayAnimation();
        

    }


    private void PlayAnimation()
    {
        if (anim == PlayerAnim.Idle)
            animator.SetBool("WillWalk", false);
        else
            animator.SetBool("WillWalk", true);
    }
}


public enum PlayerAnim
{
    Idle,Walk
}
