using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject popUpReference;
    private bool jumpKeyWasPressed, doubleJump;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        doubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
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
    private void FixedUpdate()
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
        if (other.gameObject.tag == "Final coin")
        {
            HUD.IncreaseScore(20);
            EndGame();
        } else
        {
            HUD.IncreaseScore();
        }
        Destroy(other.gameObject);
    }

    // ends the game.
    private void EndGame()
    {
        PopUp script = null;
        if (popUpReference != null)
        {
            script = popUpReference.GetComponent<PopUp>();
        }
        if (script != null)
        {
            script.GameOver(true);
        }
    } 

}
