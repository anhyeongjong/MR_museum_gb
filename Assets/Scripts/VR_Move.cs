using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR;

public class VR_Move : MonoBehaviour
{
    public Transform camTr;                       // 움직일 카메라
    public float speed;                           // 걷는 속도
    public static bool isWalking;                 // 걷는 상태

    private SteamVR_Input_Sources lefthand;       // 컨트롤러 선택 (모두, 왼손, 오른손)
    private CharacterController cc;               // rigidbody 활용x

    // 컨트롤러 버튼 타입
    public SteamVR_Action_Boolean leftMove;
    public SteamVR_Action_Boolean rightMove;
    public SteamVR_Action_Boolean forwardMove;
    public SteamVR_Action_Boolean backMove;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        lefthand = SteamVR_Input_Sources.LeftHand;  
    }

    // 이동
    private void Update()
    {
        isWalking = leftMove.GetState(lefthand) || rightMove.GetState(lefthand) || forwardMove.GetState(lefthand) || backMove.GetState(lefthand);  // 걷는 상태

        if (leftMove.GetState(lefthand))
        {
            cc.SimpleMove(camTr.right * speed * -1);
        }   
        else if (rightMove.GetState(lefthand))
        {
            cc.SimpleMove(camTr.right * speed);
        }
        else if (forwardMove.GetState(lefthand))
        {
            cc.SimpleMove(camTr.forward * speed);
        }
        else if (backMove.GetState(lefthand))
        {
            cc.SimpleMove(camTr.forward * speed * -1);
        }
    }
}
