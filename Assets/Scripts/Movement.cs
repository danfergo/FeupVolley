using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;

    // Use this for initialization
    void Start()
    {
        Input.GetAxis("Horizontal");
        Input.GetAxis("Vertical");

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(-v, 0, h) * speed;
    }
}
