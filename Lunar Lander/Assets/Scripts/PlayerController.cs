/*
Author: Simon Steele
Class: GPR-350-101
Assignment: Lab 3
Certification of Authenticity: We certify that this
assignment is entirely our own work.
*/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Particle classes
    private Particle2D particle;

    // Floats
    public float speed;
    public float thrustSpeed;
    public float startingVerticalVelocity;


    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<Particle2D>();
    }



    // Update is called once per frame
    void Update()
    {

        // Has the D key been pressed?
        if (Input.GetKey(KeyCode.D))
        {
        
            // If yes, then add torque in the right direction
            particle.ApplyTorque(new Vector2(-speed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }

        // Has the A key been pressed?
        if (Input.GetKey(KeyCode.A))
        {

            // If yes, then add torque in the left direction
            particle.ApplyTorque(new Vector2(speed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }


        if (Input.GetKey(KeyCode.W))
        {
            particle.AddForce(new Vector2(0, thrustSpeed));
        }
    }
}
