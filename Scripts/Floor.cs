using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject display;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // deleting any object that comes into contact with the floor
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        if (display != null)
        {
            PopUp displayScript = display.GetComponent<PopUp>();
            if (displayScript != null)
            {
                displayScript.GameOver(false);
            }
        }
    }
}
