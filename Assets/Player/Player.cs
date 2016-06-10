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

    public delegate void OnHealthModifiedDelegate(int health);
    public OnHealthModifiedDelegate OnHealthModified;
    public Grappler grappler;

    public int Health { get { return _health; } set { _health = value; OnHealthModified(_health);  } }
    private int _health;
    // Use this for initialization
    void Awake()
    {
        Health = 3;
    }

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
        TakeDamage();
    }

    void ReleaseGrappler()
    {

        grappler.Release();        
    }
    public void TakeDamage()
    {
        Health -= 1;
        Health = Mathf.Max(Health, 0);
    }

}
