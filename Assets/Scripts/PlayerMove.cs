using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMove : MonoBehaviour
{
    public SteamVR_Input_Sources handType;        // 컨트롤러 선택 (모두, 왼손, 오른손)
    public SteamVR_Behaviour_Pose controllerPose; // 컨트롤러 정보
    //public Transform camTr;                       // 움직일 카메라
    //public float speed;

    //private CharacterController cc;    // rigidbody 활용x
    private GameObject colliderObject; // 충돌 객체
    private GameObject objectInHand;     // 잡은 객체

    // 컨트롤러 버튼 타입
    /*public SteamVR_Action_Boolean leftMove;
    public SteamVR_Action_Boolean rightMove;
    public SteamVR_Action_Boolean forwardMove;
    public SteamVR_Action_Boolean backMove;*/
    public SteamVR_Action_Boolean grap;
    


    void Start()
    {
        //speed = 1.5f;
        //cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grap.GetLastStateDown(handType))
        {
            // 잡는 버튼 누를 때
            if(colliderObject)
            {
                GrabObject();
            }
        }
        if (grap.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
        /*if (leftMove.GetState(handType))
        {
            cc.SimpleMove(camTr.right * speed * -1);
            Debug.Log("leftMove");
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
        }*/
    }

    // 충돌이 시작되는 순간
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    // 충돌 중일 때
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }
    // 충돌이 끝나는 순간
    public void OnTriggerExit(Collider other)
    {
        if (!colliderObject)
        {
            return;
        }
        colliderObject = null;
    }
    // 충돌 중인 객체로 설정
    private void SetCollidingObject(Collider col)
    {
        if (colliderObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        colliderObject = col.gameObject;
    }

    // 객체를 잡음
    private void GrabObject() 
    {
        objectInHand = colliderObject; // 잡은 객체로 설정
        colliderObject = null;         // 충돌 객체 해제

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }
    // FoxedJoint => 객체들을 하나로 묶어 고정시켜줌
    // breakForce => 조인트가 제거되도록 하기 위한 필요한 힘의 크기
    // breakTorque => 조인트가 제거되도록 하기 위한 필요한 토크
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    // 객체를 놓음
    // contrllerpose.GetVeloecity() => 컨트롤러의 속도
    // controllerPose.GetAngularVelocity() => 컨트롤러의 각속도
    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        objectInHand = null;
    }
}
