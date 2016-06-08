using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public Door LinkedDoor;
    public bool isActive = true;
    
    // Use this for initialization
    void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            print("On door");
            if (isActive)
            {
                col.gameObject.transform.position = LinkedDoor.transform.position + new Vector3(1, 1, 0);
                
                LinkedDoor.SetInactive();//Camera.main.transform.position = 
            }
            
        }
    }
    public void SetInactive()
    {
        isActive = false;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            print("OnTriggerExit2D");
            if (isActive==false)
            {
                LinkedDoor.SetInactive();//Camera.main.transform.position = 
                isActive = true;
            }

        }
    }
}
