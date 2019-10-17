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
    // Particle classes
    private Particle2D particle;
    public static PlayerController player;

    // Floats
    public float speed;
    public float thrustSpeed;
    public float startingHorizontalVelocity;
    public float fuelLeft;
    public float amountOfFuelLostPerBurn;
    public float maximumHorizontalSpeedToPass = 5.0f;
    public float maximumVerticalSpeedToPass = 5.0f;
    public float maxRotation = 180.0f;

    // Vector 2's
    [HideInInspector] public Vector2 initialStartingSpot;

    // Start is called before the first frame update
    void Start()
    {
        player = this;
        particle = GetComponent<Particle2D>();
        particle.velocity = new Vector2(startingHorizontalVelocity, 0);
        initialStartingSpot = transform.position;
    }

    void OnEnable()
    {
        if (fuelLeft <= 0)
        {
            fuelLeft = 100;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        // Has the D key been pressed?
        if (Input.GetKey(KeyCode.D) && particle.rotation >= -maxRotation)
        {
            Debug.Log("Move");
            // If yes, then add torque in the right direction
            particle.ApplyTorque(new Vector2(-thrustSpeed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }
        else if (Input.GetKey(KeyCode.A) && particle.rotation <= maxRotation)
        {
            Debug.Log("Move");
            // If yes, then add torque in the left direction
            particle.ApplyTorque(new Vector2(thrustSpeed, 0), new Vector2(0, transform.position.y + particle.boxDimensions.y));

        }
        else if (particle.rotation > maxRotation || particle.rotation < -maxRotation)
        {
            particle.angularVelocity = 0;
        }

        if (Input.GetKey(KeyCode.W) && fuelLeft > 0)
        {
            fuelLeft -= amountOfFuelLostPerBurn;
            particle.AddForce(speed * transform.up);
        }

        if (Input.GetKey(KeyCode.S) && fuelLeft > 0)
        {
            fuelLeft -= amountOfFuelLostPerBurn;
            particle.AddForce(speed * -transform.up);
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
        UIManager.ui.ChangeFuelText(fuelLeft.ToString());
    }

    bool CheckForProperCollision()
    {
        return maximumHorizontalSpeedToPass >= particle.velocity.x && maximumVerticalSpeedToPass >= particle.velocity.y;
    }

    public void HandleCollision(CollisionHull2D a, CollisionHull2D b)
    {
        GameManager.manager.GiveLanderLandingStatus(CheckForProperCollision());
        gameObject.SetActive(false);
    }

    public void ResetPosition()
    {
        transform.position = initialStartingSpot;
        GetComponent<Particle2D>().position = initialStartingSpot;
        GetComponent<AABB>().SetPosition(initialStartingSpot);
        particle.velocity = new Vector2(startingHorizontalVelocity, 0);
    }
}
