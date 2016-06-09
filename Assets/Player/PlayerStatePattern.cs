using UnityEngine;
using System.Collections;

public class PlayerStatePattern : MonoBehaviour {

    public LayerMask whatIsGround;

    [HideInInspector]
    public IPlayerState currentState;
    [HideInInspector]
    public WalkingState walkingState;
    [HideInInspector]
    public JumpingState jumpingState;
    [HideInInspector]
    public GrabblerState grabblerState;

    private Rigidbody2D rb2D;
    private DistanceJoint2D dj2D;
    public InputManager inputMan;
    public float speed;
    public float speedWhileJumping;
    public Grappler grappler;
    public bool grounded;

    public DistanceJoint2D DJ2D { get { return dj2D; }}
    public Rigidbody2D RB2D { get { return rb2D; }}

    // Use this for initialization
    private void Awake()
    {
        walkingState = new WalkingState(this);
        jumpingState = new JumpingState(this);
        grabblerState = new GrabblerState(this);
    }

    void Start () {

        dj2D = GetComponent<DistanceJoint2D>();
        rb2D = GetComponent<Rigidbody2D>();

        currentState = walkingState;
	}
	
	// Update is called once per frame
	void Update () {
        //print(currentState);
        currentState.UpdateState();
	}
}
