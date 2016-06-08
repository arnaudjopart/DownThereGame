using UnityEngine;
using System.Collections;
using System;

public class WalkingState : IPlayerState {

    private readonly PlayerStatePattern player;
    
    
    public void Initialize()
    {

    }

    public WalkingState(PlayerStatePattern statePattern)
    {
        
        player = statePattern;
        
    }

    public void UpdateState()
    {

        bool touchWallRight = Physics2D.OverlapCircle(player.gameObject.transform.position + Vector3.right * .3f, .3f, player.whatIsGround);
        bool touchWallLeft = Physics2D.OverlapCircle(player.gameObject.transform.position + Vector3.left * .3f, .3f, player.whatIsGround);

        player.RB2D.velocity = new Vector3(player.inputMan.Direction.x * player.speed, player.RB2D.velocity.y, 0);
        if (player.inputMan.jump)
        {
            ToJumpingState();
        }
        if (touchWallLeft)
        {
            //Debug.Log("Touch wall");
        }
    }
    public void ToJumpingState()
    {
        player.RB2D.AddForce(Vector3.up * 50f, ForceMode2D.Impulse);
        player.currentState = player.jumpingState;
    }
    public void ToWalkingState()
    {
        Debug.Log("Alerady in that state");           
    }
   public void ToGrabblerState()
    {

    }

}
