/*
Author: Simon Steele
Class: GPR-350-101
Assignment: Lab 3
Certification of Authenticity: We certify that this
assignment is entirely our own work.
*/

using UnityEngine;

public class PlayerController : MonoBehaviour, CollisionEvent
{
    // Singleton Instances
    public static PlayerController player;
    public GameObject explosion;

    // Particle classes
    private Particle2D particle;

    // Gameobject's
    public GameObject flame;

    // Floats
    public float speed;
    public float thrustSpeed;
    public float startingHorizontalVelocity;
    public float startingFuelLeft;
    public float amountOfFuelLostPerBurn;
    public float maximumHorizontalSpeedToPass = 5.0f;
    public float maximumVerticalSpeedToPass = 5.0f;
    public float maximumRotationToPass = 10.0f;
    public float minimumRotationToPass = -170.0f;
    public float maxRotation = 180.0f;
    [HideInInspector] public float fuelLeft;

    // Vector 2's
    [HideInInspector] public Vector2 initialStartingSpot;





    // Start is called before the first frame update
    void Awake()
    {
        player = this;
        particle = GetComponent<Particle2D>();
        particle.velocity = new Vector2(startingHorizontalVelocity, 0);
        initialStartingSpot = transform.position;
    }





    // This function will execute whenever the level resets
    void OnEnable()
    {
        // Is the fuel less than 0?
        if (fuelLeft <= 0)
        {
            // If yes, then reset it back to 100
            fuelLeft = 100;
        }
    }





    // Update is called once per frame
    void FixedUpdate()
    {

        // Has the D key been pressed?
        if (Input.GetKey(KeyCode.D) && particle.rotation >= -maxRotation)
        {
            // If yes, then add torque in the right direction
            particle.ApplyTorque(new Vector2(-thrustSpeed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }

        // Has the A key been pressed?
        else if (Input.GetKey(KeyCode.A) && particle.rotation <= maxRotation)
        {
            // If yes, then add torque in the left direction
            particle.ApplyTorque(new Vector2(thrustSpeed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }

        // Has the lander reached it's maximum rotation?
        else if ((particle.rotation > maxRotation || particle.rotation < -maxRotation) && GameManager.manager.isRunning)
        {
            // If yes, then stop it in place
            particle.angularVelocity = 0;
        }

        // Has the W key been pressed and there is enough fuel?
        if (Input.GetKey(KeyCode.W) && fuelLeft > 0 && GameManager.manager.isRunning)
        {
            // If yes, then subtract fuel
            fuelLeft -= amountOfFuelLostPerBurn;

            // Add a force in the upward direction of the lander
            particle.AddForce(speed * transform.up);

            // Enable flame particle effect
            flame.SetActive(true);
        }
        else
        {
            // If no, then disable the flame particle effect
            flame.SetActive(false);
        }

        // Has the S key been pressed and there is enough fuel?
        if (Input.GetKey(KeyCode.S) && fuelLeft > 0)
        {
            // If yes, then subtract the fuel
            fuelLeft -= amountOfFuelLostPerBurn;

            // Add a force in the downward direction of the lander
            particle.AddForce(speed * -transform.up);
        }
    }





    private void Update()
    {
        // Is fuel left less than 0?
        if (fuelLeft < 0)
        {
            // If yes, then set it back to 0
            fuelLeft = 0;
        }

        // Update all UI's relating to player
        UpdateTexts();
    }





    // This function updates all of the UI's with the proper Player information
    void UpdateTexts()
    {
        UIManager.ui.ChangeText(UIManager.HORIZONTAL_NAME, (Mathf.Abs(GetComponent<Particle2D>().velocity.x)).ToString());
        UIManager.ui.ChangeText(UIManager.VERTICAL_NAME, (Mathf.Abs(GetComponent<Particle2D>().velocity.y)).ToString());
        UIManager.ui.ChangeText(UIManager.FUEL_NAME, fuelLeft.ToString());
    }





    // This function checks if the player has properly landed the lunar lander
    bool CheckForProperCollision()
    {
        bool velocityCheck = maximumHorizontalSpeedToPass >= particle.velocity.x && maximumVerticalSpeedToPass >= particle.velocity.y;
        bool rotationCheck = maximumRotationToPass >= particle.rotation && minimumRotationToPass <= particle.rotation;
        return velocityCheck && rotationCheck;
    }





    // This function is an event that gets triggered whenever the player is colliding with something
    public void HandleCollision(CollisionHull2D a, CollisionHull2D b)
    {
        // Check if the lander has landed correctly
        bool checkApproval = CheckForProperCollision();

        // Has the lander landed correctly?
        if (!checkApproval)
        {
            // If yes, then shoot it up into the air
            particle.angularVelocity = 1000;
        }

        // Pass findings onto Game Manager
        GameManager.manager.ProcessLanderLandingStatus(checkApproval);
    }





    // This function resets the transformation of the player
    public void ResetPosition()
    {
        // Reset Position
        transform.position = initialStartingSpot;
        GetComponent<Particle2D>().position = initialStartingSpot;
        GetComponent<AABB>().SetPosition(initialStartingSpot);

        // Reset Angular Velocity
        particle.angularVelocity = 0;

        // Reset Velocity
        particle.velocity = new Vector2(startingHorizontalVelocity, 0);

        // Reset Rotation
        transform.rotation = Quaternion.identity;
        particle.rotation = 0;
    }
}
