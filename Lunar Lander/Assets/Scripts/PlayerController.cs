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
    public float startingHorizontalVelocity;


    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<Particle2D>();
        particle.velocity = new Vector2(startingHorizontalVelocity, 0);
    }



    // Update is called once per frame
    void FixedUpdate()
    {

        // Has the D key been pressed?
        if (Input.GetKey(KeyCode.D))
        {
        
            // If yes, then add torque in the right direction
            particle.ApplyTorque(new Vector2(-thrustSpeed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }

        // Has the A key been pressed?
        if (Input.GetKey(KeyCode.A))
        {

            // If yes, then add torque in the left direction
            particle.ApplyTorque(new Vector2(thrustSpeed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }


        if (Input.GetKey(KeyCode.W))
        {

            particle.AddForce(speed * transform.up);

        }
    }

    private void Update()
    {
        UpdateTexts();
    }

    Vector2 FindDirectionalVector(float rotation)
    {
        float x = -Mathf.Sin(rotation);
        float y = Mathf.Cos(rotation);
        Debug.Log(new Vector2(x, y));
        return new Vector2(x, y);
    }

    void UpdateTexts()
    {
        UIManager.ui.ChangeHorizontalSpeedText((Mathf.Abs(GetComponent<Particle2D>().velocity.x)).ToString());
        UIManager.ui.ChangeVerticalSpeedText((Mathf.Abs(GetComponent<Particle2D>().velocity.y)).ToString());
    }
}
