using UnityEngine;
using System.Collections;

public class GrabblerState : IPlayerState {

    private PlayerStatePattern player;

    public GrabblerState(PlayerStatePattern pattern)
    {
        player = pattern;
    }

    public void Initialize()
    {
        player.DJ2D.enabled = true;
        
        
    }
    public void UpdateState()
    {
        player.RB2D.AddForce(new Vector3(player.inputMan.Direction.x * player.speedWhileJumping, 0, 0));

        if (player.grappler.isHooked == false)
        {
            player.DJ2D.enabled = false;
            ToJumpingState();
        }else
        {
            player.DJ2D.connectedAnchor = player.grappler.headOfRope;
        }
    }
    public void ToWalkingState()
    {

    }
    public void ToJumpingState()
    {
        player.currentState = player.jumpingState;
    }
    public void ToGrabblerState()
    {

    }
}
