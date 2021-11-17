using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class UiManager : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject layserPrefab;
    private GameObject layser;
    private Transform layserTransform;
    private Vector3 hitPoint;

    private int layerMask;//Raycast가 식별할 레이어

    public bool RaycastCheck;//Raycast가 물체를 충돌했는가 체크

    public RaycastHit hit;

    public float LayserDistance = 100f;

    public Text Legacy_name;
    // Start is called before the first frame update
    void Start()
    {
        layser = Instantiate(layserPrefab);
        layserTransform = layser.transform;

        layerMask = 1 << 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            if (Physics.Raycast(controllerPose.transform.position, transform.forward,
                out hit, LayserDistance, layerMask))
            {
                hitPoint = hit.point;
                RaycastCheck = true;
            }

            else
            {
                RaycastCheck = false;
            }
            ShowLayser(hit);

        }
        else
        {
            RaycastCheck = false;
            layser.SetActive(false);
            Legacy_name.gameObject.SetActive(false);
            Legacy_name.text = null;
        }
    }
    public void ShowLayser(RaycastHit hit)
    {
        layser.SetActive(true);

        layser.transform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);
        layserTransform.LookAt(hitPoint);
        layserTransform.localScale = new Vector3(layserTransform.localScale.x, layserTransform.localScale.y, hit.distance);


        Legacy_name.gameObject.SetActive(true);
        Legacy_name.text = hit.transform.gameObject.name;

    }
}
