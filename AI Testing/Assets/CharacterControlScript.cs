using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControlScript : MonoBehaviour
{
  //Variables

	private Vector2 mD;						//mD stands for Mouse Direction
	private float mouseXValue = 0.0f;		//Used to store inputs recieved from mouse of X & Y Values
    private float mouseYValue = 0.0f;		//Used to store inputs recieved from mouse of X & Y Values
	public float mouseXSpeed = 2.0f;		//Controls the sensitivity of the mouse on the X & Y Axis
    public float mouseYSpeed = 2.0f;		//Controls the sensitivity of the mouse on the X & Y Axis
    public float forwardSpeed = 10f;		//Speed at which the player moves forwards
    public float backwardSpeed = 5f;		//Speed at which the player moves backwards
    public float strafeSpeed = 5f;			//Speed at which the player moves sideways
    public float jumpForce = 5.0f;			//Force upwards the player jumps
    public float runMultiplier = 2.0f;		//The multiplier used while running
    public float crouchMultiplier = 0.5f;	//The mutiplier used while crouching
    public float crouchCameraMove = 0.2f;	//The amount the camera moves down when the player crouches
    public GameObject fixedYAxis;			//Used to make the camera able to look up and down the Y axis without the object rotating
    public GameObject cameraCrouch;			//Used to make the camera translate downwards to simulate crouching
    private Rigidbody rb;					//The rigidbody of the object, used in jumping AddForce
    public CapsuleCollider col;				//Used to calculate bounds of object for isGrounded function
    public LayerMask groundLayers;			//Used in isGrounded function to define what types of groud layers it should count (set to default)
    private bool isGrounded()				//Checks whether the object is currently touching the ground and returns true or false
    {
    	return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
    }


      //UI Variables

    public Slider healthSlider;
    public int maxHealth;

    public Slider energySlider;
    public int maxEnergy;

    public Slider oxygenSlider;
    public int maxOxygen;


      // Start is called before the first frame update
    void Start()
    {

    	rb = GetComponent<Rigidbody>();
    	col = GetComponent<CapsuleCollider>();


        //Making the cursor invisible & Locking it in the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        //Initializing player vitals
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy;

        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = maxOxygen;

    }

    // Update is called once per frame
    void Update()
    {

   	

    	// Mouse Inputs

        mouseXValue += mouseXSpeed * Input.GetAxis("Mouse X");
        mouseYValue -= mouseYSpeed * Input.GetAxis("Mouse Y");
        mouseYValue = Mathf.Clamp(mouseYValue, -90, 90);	//Clamping the Y axis to stop it exceeding the 180 degrees in front of you

        transform.eulerAngles = new Vector3(0, mouseXValue); //transforming the X & Y axis of the object itself   
        fixedYAxis.transform.eulerAngles = new Vector3(mouseYValue, mouseXValue); //transorming the X & Y axis of the camera (child)


    	// Keyboard Inputs


        //Running

       	if(Input.GetKeyDown(KeyCode.LeftShift))
       	{
       		forwardSpeed *= runMultiplier;
        }

       	if(Input.GetKeyUp(KeyCode.LeftShift))
       	{ 
       		forwardSpeed /= runMultiplier;
        }


        //Crouching

        if(Input.GetKeyDown(KeyCode.LeftControl))
       	{
       		forwardSpeed *= crouchMultiplier;
       		cameraCrouch.transform.Translate(Vector3.down * crouchCameraMove);	//Makes the camera transform downwards to simulate crouching
        }

       	if(Input.GetKeyUp(KeyCode.LeftControl))
       	{ 
       		forwardSpeed /= crouchMultiplier;
       		cameraCrouch.transform.Translate(Vector3.up * crouchCameraMove);
        }


        //Jumping

        if(isGrounded() && Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W))
       	{
       		rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);       		
        }
        else if(isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
        	rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }      
      	

        //Standard movement keys

    	if(Input.GetKey(KeyCode.W))
    	{
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    	}
        
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * backwardSpeed * Time.deltaTime);
        }
        
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * strafeSpeed * Time.deltaTime);
        }
        
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * strafeSpeed * Time.deltaTime);
        }
          
        
    }
}
