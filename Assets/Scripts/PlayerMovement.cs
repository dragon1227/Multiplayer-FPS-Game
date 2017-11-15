using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Rigidbody rb;

    [SerializeField]
    public Camera c;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }//start

    public void Move(Vector3 v)
    {
        velocity = v;
    }// movement vectors

    public void Rotate(Vector3 r)
    {
        rotation = r;
    }// rotational vectors

    public void RotateCamera(Vector3 cr)
    {
        cameraRotation = cr;
    }

    public void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void RotatePlayer()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        
        if(c != null)
        {
            c.transform.Rotate(-cameraRotation);
        }
    }



}
