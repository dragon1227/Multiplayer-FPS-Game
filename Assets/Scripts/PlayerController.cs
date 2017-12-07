using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    private float sensitivity = 1f;

    private PlayerMovement move;


    private void Start()
    {
        move = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Calculate player movement & rotation as a vector 3
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        float rotY = Input.GetAxisRaw("Mouse X");
        float rotX = Input.GetAxisRaw("Mouse Y");

        // movement vectors 
        Vector3 moveHor = transform.right * moveX; 
        Vector3 moveVer = transform.forward * moveZ;
        Vector3 velocity = (moveHor + moveVer).normalized * speed;

        // rotation vectors - turns player around
        Vector3 rotation = new Vector3(0f, rotY, 0f) * sensitivity;
        float cameraRotation = rotX * sensitivity;

        move.Move(velocity); // apply velocity to the player movement class
        move.Rotate(rotation); // apply rotation to the player movement class
        move.RotateCamera(cameraRotation); // apply the camera roatation to player movement class
      


    }


}
