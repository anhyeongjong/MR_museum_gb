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

    public GameObject controller_obj;
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
            Debug.Log("Check Input");
            if (Physics.Raycast(controllerPose.transform.position, controllerPose.transform.forward,
                out hit, LayserDistance, layerMask))
            {
                hitPoint = hit.point;
                RaycastCheck = true;
                Legacy_name.gameObject.SetActive(true);
                Legacy_name.text = hit.transform.gameObject.name;
            }

            else
            {
                RaycastCheck = false;
                Legacy_name.gameObject.SetActive(false);
                Legacy_name.text = null;
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
        if(RaycastCheck)
        {
            layser.transform.position = Vector3.Lerp(controllerPose.transform.position, hit.point, 0.5f);
            layserTransform.LookAt(hit.point);
            layserTransform.localScale = new Vector3(layserTransform.localScale.x, layserTransform.localScale.y, hit.distance);

        }
        
        else
        {
            Vector3 hit_point = controllerPose.transform.position + controllerPose.transform.forward * LayserDistance;
            //layser.transform.position = controllerPose.transform.position;
            layser.transform.position = Vector3.Lerp(controllerPose.transform.position, hit_point, 0.5f);
            layserTransform.LookAt(hit_point);
            layserTransform.localScale = new Vector3(layserTransform.localScale.x, layserTransform.localScale.y, LayserDistance);
        }

    }
}
