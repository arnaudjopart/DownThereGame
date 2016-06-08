using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    
    public float speed;
    public GameObject target;
    public Vector3 offSet;
    public Vector3 velocity;
    public float smoothTime;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize--;
        }

        Vector3 targetPosition = target.transform.TransformPoint(offSet);

        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
    }
}
