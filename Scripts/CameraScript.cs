using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] private Transform following = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (following != null)
        {
            Vector3 oldPos = transform.position;
            Vector3 newPos = new Vector3(following.position.x, following.position.y, oldPos.z);
            transform.position = newPos;
        }
    }
}
