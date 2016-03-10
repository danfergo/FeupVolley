using UnityEngine;
using System.Collections;

public class SlowGravity : MonoBehaviour {

    Rigidbody rb;
    public float antiGravity;

    private LineRenderer lineRenderer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate () {
            rb.AddForce(Physics.gravity* -1 * antiGravity * rb.mass);


        predict(this.transform.position, this.GetComponent<Rigidbody>().velocity, new Vector3(0, -10, 0));

    }
   /* void OnCollisionExit(Collision other) {
      //      if(other === )
        Debug.Log("xxx");

    }*/


    void predict(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity) {
        
        Vector3 last_pos = initialPosition;
        Vector3 velocity = initialVelocity;
        lineRenderer.SetVertexCount(1);
        lineRenderer.SetPosition(0, last_pos);
        int i = 1;
        int hitCounter = 0;
        while (i < 150)
        {
            velocity += gravity * Time.fixedDeltaTime;
            RaycastHit hit;
            if (Physics.Linecast(last_pos, (last_pos + (velocity * Time.fixedDeltaTime)), out hit))
            {
                velocity = Vector3.Reflect(velocity * 0.9f, hit.normal);
                last_pos = hit.point;
                hitCounter++;

                if (hitCounter == 2) {
                    return;
                }
                //hit.collider.gameObject
            }
            lineRenderer.SetVertexCount(i + 1);
            lineRenderer.SetPosition(i, last_pos);
            last_pos += velocity * Time.fixedDeltaTime;
            i++;
        }
        

        /* int numSteps = 5; // for example
        Vector3 velocity = initialVelocity;
        Vector3 last_pos = initialPosition;

        lineRenderer.SetVertexCount(1);
        lineRenderer.SetPosition(0, last_pos);
        int i = 1;
        while (i < numSteps)
        {
            velocity += gravity * Time.fixedDeltaTime;
            RaycastHit hit;
            if (Physics.Linecast(velocity, (velocity + (initialVelocity * Time.fixedDeltaTime)), out hit))
            {
                velocity = Vector3.Reflect(initialVelocity * 0.5f, hit.normal);
                last_pos = hit.point;
            }
            lineRenderer.SetVertexCount(i + 1);
            lineRenderer.SetPosition(i, last_pos);
            last_pos += velocity * Time.fixedDeltaTime;
            i++;
        }*/
    }


    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        int numSteps = 15; // for example
        float timeDelta = .5f / initialVelocity.magnitude; // for example
            
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
