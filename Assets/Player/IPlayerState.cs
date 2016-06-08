using UnityEngine;
using System.Collections;

public interface IPlayerState {


    void Initialize();
    void UpdateState();
    void ToWalkingState();
    void ToJumpingState();
    void ToGrabblerState();


}
