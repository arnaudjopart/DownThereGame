using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public InputManager inputManager;
    public float speed;
    private Rigidbody2D rb2D;
    
    private Vector3 headOfRope;
    private Ray ropeDetector;
    private float lengthOfRope;
    private HingeJoint2D hj2D;
    private DistanceJoint2D dj2D;
    private SpringJoint2D sj2D;
    public LayerMask whatIsGround;

    public Grappler grappler;

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        
        dj2D = GetComponent<DistanceJoint2D>();
        
    }
	
	// Update is called once per frame
	void Update () {

        grappler.gameObject.transform.position = this.transform.position;

              
        if (inputManager.leftMouseButtonDown)
        {
            Shoot();
        }
        if (inputManager.rightMouseButton)
        {
            ReleaseGrappler();
        }

    }
    void Shoot()
    {
        grappler.Shoot(transform.position, inputManager.mousePosition,whatIsGround);
        
    }

    void ReleaseGrappler()
    {

        grappler.Release();        
    }

}
