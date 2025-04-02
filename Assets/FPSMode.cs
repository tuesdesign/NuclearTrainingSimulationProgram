using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public class FPSMode : MonoBehaviour
{
    Vector3 CAMTHIRDPERSONPOS = new Vector3(-2.8f, 30.9f, 32.2f);
    Vector3 CAMTHIRDPERSONROT = new Vector3(90.0f, -90.0f, 0.0f);

    private Vector2 horizontalInput;
    private Vector3 verticalVelocity = Vector3.zero;
    private float xClamp = 85f;
    private float xRotation = 0.0f;

    private Vector2 mouseInput;
    bool isGrounded;
    [SerializeField]
    LayerMask groundMask;

    float sensitivityX = 8f;
    float sensitivityY = 0.5f;
    float mouseX, mouseY;


    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI availableText;
    bool pickMode = false;
    GameObject[] POIs;
    bool firstPersonMode = false;
    public Button buttonRef;

    [SerializeField]
    InputActionReference cameraXMovement;
    [SerializeField]
    InputActionReference cameraYMovement;
    [SerializeField]
    InputActionReference playerMovement;
    [SerializeField]
    InputActionReference Click;

    public CharacterController controller;


    

    // Start is called before the first frame update
    void Start()
    {
        POIs = GameObject.FindGameObjectsWithTag("POI");
        Click.action.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.x);
        if (pickMode)
        {
            //buttonRef.SetEnabled(false);
            buttonText.text = "Choose your Point of Interest";
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            //Debug.Log(worldPosition);

            for(int i = 0; i < POIs.Length; i++)
            {
                //Debug.Log("Mouse X position: " + Mathf.Round(worldPosition.x) + " Mouse Z position: " + Mathf.Round(worldPosition.z));
                //Debug.Log("Point of Interest #" + 1 + "'s x position: " + Mathf.Round(POIs[1].gameObject.transform.position.x) + " Point of Interest #" + 1 + "'s z position: " + Mathf.Round(POIs[1].gameObject.transform.position.z));
                //Debug.Log(worldPosition.y);
                //Debug.Log(POIs[1].gameObject.transform.position.y);
                if (Mathf.Round(worldPosition.x) == Mathf.Round(POIs[i].gameObject.transform.position.x) && Mathf.Round(worldPosition.z) == Mathf.Round(POIs[i].gameObject.transform.position.z))
                {
                    //Debug.Log(POIs[i]);
                    availableText.text = ("Place yourself at Point of Interest #" + i + "?");
                    availableText.gameObject.transform.position = screenPosition;
                    if (Click.action.WasPressedThisFrame())
                    {
                        //Debug.Log("Clicked on a POI, LERP camera");
                        Camera.main.orthographic = false;
                        Camera.main.fieldOfView = 70;
                        //Debug.Log("Player's X position before: " + transform.position.x);
                        transform.position = POIs[i].gameObject.transform.position;
                        //Debug.Log("Player's X position after: " + transform.position.x);
                        this.transform.eulerAngles = new Vector3(0, 0, 0);
                        firstPersonMode = true;
                        pickMode = false;
                        StartCoroutine(ControllerWait());
                    }
                }
                else { //availableText.text = "";
                       }

            }
        }
        else
        {
            //buttonRef.SetEnabled(true);
            buttonText.text = "First Person Mode";

        }
        if (firstPersonMode)
        {

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
            if (isGrounded)
            {
                verticalVelocity.y = 0;
            }
            //Debug.Log("Before the FPS calculations: " + transform.position.x);

            playerMovement.action.Enable();
            playerMovement.action.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
            cameraXMovement.action.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
            cameraYMovement.action.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

            mouseX = mouseInput.x * 8.0f;
            mouseY = mouseInput.y * 0.5f;
            //Debug.Log("Before the FPS calculations: " + transform.position.x);

            //Debug.Log("Before the FPS calculations: " + transform.position.x);
            Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * 11;

            controller.Move(horizontalVelocity * Time.deltaTime);
            //Debug.Log("After the FPS calculations: " + transform.position.x);


            verticalVelocity.y += -30.0f * Time.deltaTime;
            controller.Move(verticalVelocity * Time.deltaTime);

            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;
            Camera.main.transform.eulerAngles = targetRotation;
            //Debug.Log("After the FPS calculations: " + transform.position.x);


        }
        else
        {
            transform.position = CAMTHIRDPERSONPOS;
            transform.eulerAngles = CAMTHIRDPERSONROT;
            Camera.main.orthographic = true;
            Camera.main.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            cameraXMovement.action.Disable();
            cameraYMovement.action.Disable();
            playerMovement.action.Disable();
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }

    }

    public void onPickModeClick()
    {
        pickMode = true;
        firstPersonMode = false;
    }

    IEnumerator ControllerWait()
    {
        availableText.text = "Spawning in 3 seconds...";
        controller.enabled = false;
        yield return new WaitForSeconds(3);
        controller.enabled = true;
        availableText.text = "";
        cameraXMovement.action.Enable();
        cameraYMovement.action.Enable();
    }

}
