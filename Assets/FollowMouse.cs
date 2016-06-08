using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePosition =  new Vector3();
        mousePosition = Input.mousePosition;
        mousePosition.Set(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z-3);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //print(mousePosition);
        transform.position = mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            //print(transform.position);
        }
	}
}
