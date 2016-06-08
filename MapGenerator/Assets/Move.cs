using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    Vector3 movement;
    Vector3 direction;
    public float speed;

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update () {
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
        movement = direction * speed*Time.deltaTime;
        transform.position += movement;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize--;
        }
    }
}
