using UnityEngine;
using System.Collections;
using System;

public class JumpingState : IPlayerState {


    private PlayerStatePattern player;

    public JumpingState(PlayerStatePattern pattern)
    {
        player = pattern;
    }

    public void Initialize()
    {

    }
    public void UpdateState()
    {
        
        bool grounded = Physics2D.OverlapCircle(player.gameObject.transform.position + Vector3.down * .7f, .3f, player.whatIsGround);
        player.RB2D.AddForce(new Vector3(player.inputMan.Direction.x * player.speedWhileJumping, 0, 0));
        
        if (grounded)
        {
            ToWalkingState();
        }
        if (player.grappler.isHooked)
        {
            ToGrabblerState();  
        }
    }
    public void ToWalkingState()
    {
        Debug.Log("ToWalkingState");
        player.currentState = player.walkingState; 
    }
    public void ToJumpingState()
    {

    }
    public void ToGrabblerState()
    {
        Debug.Log("ToGrabblerState");
        player.currentState = player.grabblerState;
        player.currentState.Initialize();
    }
}
