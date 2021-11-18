using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR;

public class VR_move : MonoBehaviour
{
    public SteamVR_Input_Sources handType;        // 컨트롤러 선택 (모두, 왼손, 오른손)
    public Transform camTr;                       // 움직일 카메라
    public float speed;
    public static bool isWalking;                 // 걷는 상태

    private CharacterController cc;               // rigidbody 활용x

    // 컨트롤러 버튼 타입
    public SteamVR_Action_Boolean leftMove;
    public SteamVR_Action_Boolean rightMove;
    public SteamVR_Action_Boolean forwardMove;
    public SteamVR_Action_Boolean backMove;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {
        isWalking = leftMove.GetState(handType) || rightMove.GetState(handType) || forwardMove.GetState(handType) || backMove.GetState(handType);  // 걷는 상태

        if (leftMove.GetState(handType))
        {
            cc.SimpleMove(camTr.right * speed * -1);
        }   
        else if (rightMove.GetState(handType))
        {
            cc.SimpleMove(camTr.right * speed);
        }
        else if (forwardMove.GetState(handType))
        {
            cc.SimpleMove(camTr.forward * speed);
        }
        else if (backMove.GetState(handType))
        {
            cc.SimpleMove(camTr.forward * speed * -1);
        }
    }
}
