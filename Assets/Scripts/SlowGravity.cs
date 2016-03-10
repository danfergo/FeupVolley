using UnityEngine;
using System.Collections;

public class SlowGravity : MonoBehaviour {

    Rigidbody rb;
    public float antiGravity;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {
            rb.AddForce(Physics.gravity* -1 * antiGravity * rb.mass);


    }

    void Update() {
        UpdateTrajectory(this.transform.position, this.GetComponent<Rigidbody>().velocity, new Vector3(0, -10, 0));

    }

    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        int numSteps = 100; // for example
        float timeDelta = 1.0f / initialVelocity.magnitude; // for example
            
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numSteps);

        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;
        for (int i = 0; i < numSteps; ++i)
        {
            lineRenderer.SetPosition(i, position);

            position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
            velocity += gravity * timeDelta;
        }
    }
}
