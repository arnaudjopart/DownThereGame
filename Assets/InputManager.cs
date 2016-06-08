using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public Vector3 movement;
    public Vector3 direction;

    public Vector3 Direction
    {
        get { return direction; }
    }

    public Vector3 mousePosition = new Vector3();
    public bool rightMouseButton,leftMouseButtonDown, jump;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        ManageKeyboard();
        ManageMouse();
        
        //movement = direction * speed * Time.deltaTime;
        //transform.position += movement;

    }
    private void ManageKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            direction.x = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction.x = 1;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            direction.y = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction.y = -1;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            direction.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            direction.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            direction.y = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            direction.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            
        }
        else 
        {
            jump = false;
        }
    }
    void ManageMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseButtonDown = true;
        }
        if(Input.GetMouseButtonUp(0)) 
        {
            leftMouseButtonDown = false; ;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightMouseButton = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            rightMouseButton = false; ;
        }

        mousePosition = Input.mousePosition;
        mousePosition.Set(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //print(mousePosition);
        transform.position = mousePosition;
        
    } 
}
