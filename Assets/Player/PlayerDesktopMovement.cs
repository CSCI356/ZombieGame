using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesktopMovement : MonoBehaviour
{
    [SerializeField] private float MOVE_SPEED = 20.0f;
    [SerializeField] private float ROTATE_SPEED = 80.0f;

    Animator animator;

    private float steppingSpeed = 0.2f;
    private float coolDown = 0;

    Rigidbody rigidbody;

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * MOVE_SPEED;
        float ang = Input.GetAxis("Horizontal") * Time.deltaTime * ROTATE_SPEED;

        transform.Rotate(0, ang, 0);

        if(moveZ != 0){
            //Debug.Log("Running");
            animator.SetTrigger("isRunning");
            
            if(coolDown <= 0){
                coolDown = steppingSpeed;
                SoundFXManager.Instance.PlayWalkingSound();
            }else{
                coolDown -= Time.deltaTime;
            }
        }

        // NOTE: using transform.Translate is 'smoother' but allows player 
        // to break through boundaries
        // TODO: Ideally, we want to use the force so the player cannot break
        // through the boundaries BUT currently this makes the camera jolty
        transform.Translate(0, 0, moveZ);
        // rigidbody.AddForce(transform.forward * moveZ, ForceMode.Impulse);
    }
}
