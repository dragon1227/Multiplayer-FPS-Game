using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float camRotX = 0f;
    private float currRot = 0f;
    private float rotationLock = 75f;
   
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

    public void RotateCamera(float cr)
    {
        camRotX = cr;
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
        }//if 

    }

    private void RotatePlayer()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        
        if(c != null)
        {
            currRot -= camRotX; // set rotation to - cam rot so it wont be inverse
            currRot = Mathf.Clamp(currRot, -rotationLock, rotationLock); // clamp the rotation

            c.transform.localEulerAngles = new Vector3(currRot, 0f, 0f); // apply the rotation
        }
    }



}
