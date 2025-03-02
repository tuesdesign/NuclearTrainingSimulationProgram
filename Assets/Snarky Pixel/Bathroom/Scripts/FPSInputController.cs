using UnityEngine;
using System.Collections;

[AddComponentMenu("Character/FPS Input Controller")]
public class FPSInputController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    public Light flashlight;

    public Rigidbody TPRoll;
    public float TPSpeed = 10f;

    private Vector3 moveDirection = Vector3.zero;
    private bool doRun = false;

    void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed * (doRun ? 1.5f : 1.0f);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            TPAttack();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            doRun = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            doRun = false;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void TPAttack()
    {
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        Rigidbody rocketClone = (Rigidbody)Instantiate(TPRoll, screenCenter, transform.rotation);
        rocketClone.velocity = Camera.main.transform.forward * TPSpeed;
    }
}
