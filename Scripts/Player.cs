using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject level;
    private static float ERROR_MARGIN = 0.0001f;
    private bool jumpKeyWasPressed, doubleJump, ignoreControls;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        originalPos = rigidBodyComponent.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (ignoreControls)
        {
            return;
        }
        if (Math.Abs((Input.GetAxis("Jump") - 1)) < ERROR_MARGIN)
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    /*
     * Fixed update is run every time that the physics update. By default it is 100 hz
     * Keep key inputs inside the update methods because you could miss a key press
     * as the fixed update is not called in the same cycle. Try to keep all physics 
     * changes in fixed update as well. 
     * */
     void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * 1.5f, rigidBodyComponent.velocity.y, 0);

        bool freeFloating;
        freeFloating = Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0;
        
        // will also check if double jumping is available. It will only be available if it is free floating and has 
        // not jumped already. 
        if (jumpKeyWasPressed && !(freeFloating && !doubleJump))
        {
            rigidBodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
            doubleJump = !freeFloating;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinalCoin")
        {
            HUD.IncreaseScore(20);
            Debug.Log("EndingGame");
            EndGame();
        } else
        {
            HUD.IncreaseScore();
        }
        Destroy(other.gameObject);
    }

    /*
     * Resets the player to the postion that was before the game started
     * */
    public void ResetPlayer()
    {
        float xAngle, yAngle, zAngle;
        xAngle = transform.eulerAngles.x;
        yAngle = transform.eulerAngles.y;
        zAngle = transform.eulerAngles.z;

        transform.Rotate(-xAngle, -yAngle, -zAngle);
        transform.position = originalPos;
        rigidBodyComponent.freezeRotation = true;
        rigidBodyComponent.isKinematic = false;
        ignoreControls = false;
    }

    public void SetIgnoreControl(bool ignoreControls)
    {
        this.ignoreControls = ignoreControls;
    }

    // ends the game.
    private void EndGame()
    {
        level.GetComponent<Level>().EndGame(true);
    }

}
