using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float _characterMovementSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementX(), 0, movementZ());
    }

    float movementX()
    {
        return Input.GetAxis("Horizontal") * _characterMovementSpeed * Time.deltaTime;
    }

    float movementZ()
    {
        return Input.GetAxis("Vertical") * _characterMovementSpeed * Time.deltaTime;
    }
}
