using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LoadText : MonoBehaviour
{
    public UiManager uiManager;

    public GameObject UI1;

    public GameObject Legacy;

    //[Range(0.01f, 0.1f)] public float textDelay;//텍스트 표기 속도

    public Text[] Legacy_Text;//표시할 텍스트
    public Text Legacy_Info_text;

    private bool Checking_On_off; //버튼 온오프

    private Coroutine runningCoroutine;

    public RaycastHit hit;//Raycast로 피격한 오브젝트 정보

    float MaxDistance = 300f;//Raycast 사정거리

    public SteamVR_Action_Boolean teleportAction;

    public SteamVR_Input_Sources handType;

    public SteamVR_Action_Boolean Up;
    public SteamVR_Action_Boolean down;

    public GameObject UIComponent;

    private ScrollViewChange SVC;
    //private int layerMask;//Raycast가 식별할 레이어

    //private bool RaycastCheck;//Raycast가 물체를 충돌했는가 체크

    // Start is called before the first frame update
    void Start()
    {
        //TreePicture = GameObject.Find("TreePicture");
        Checking_On_off = false;
        UI1.gameObject.SetActive(false);
        StartCoroutine(ViewInfo());
        SVC = Legacy_Info_text.gameObject.GetComponent<ScrollViewChange>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadText_toggle()
    {
        Checking_On_off = !Checking_On_off;


        string name = null ;
        try
        { 
            name = uiManager.hit.transform.gameObject.name; 
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("NullReferenceException");
        }

        if (Checking_On_off == true && uiManager.RaycastCheck ==true)
        {
            InfoUI_toggle();

            string txtData = null;
            if (name == null)
            {
                Debug.Log("충돌 물체 없음");
                return;
            }
            txtData = GameObject.Find(name).GetComponent<Text>().text;
            Legacy_Info_text.text = txtData;
        }
        else
        {
            if (uiManager.Legacy_name.gameObject.activeSelf ==true)
            {
                InfoUI_toggle();
            }
            else if (uiManager.RaycastCheck == false)
            {
                Checking_On_off = !Checking_On_off;
            }
            Debug.Log("NullReferenceException");
               
        }
        
    }



   /* public IEnumerator InputText(string txtData)
    {
        Debug.Log(txtData.Length);

        for(int i=0;i<=txtData.Length;i++)
        {
            Legacy_Info_text.text = txtData.Substring(0, i);
            yield return new WaitForSecondsRealtime(textDelay);
        }

        /*for(int i=0;i< txtData.Length;i++)
        {
            for (int j = 0; j <= txtData[i].Length; j++)
            {
                treeText[i].text = txtData[i].Substring(0, j);
                yield return new WaitForSecondsRealtime(textDelay);

            }
        }
    }*/
    public IEnumerator ViewInfo()
    {
        do
        {
            if (teleportAction.GetState(handType))
            {
                Debug.Log("ViewInfo");
                LoadText_toggle();
                yield return new WaitForSecondsRealtime(0.4f);
            }
            if(Up.GetState(handType))
            {
                SVC.ScrollUp();
                yield return new WaitForSecondsRealtime(0.5f);
            }
            else if(down.GetState(handType))
            {
                SVC.ScrollDown();
                yield return new WaitForSecondsRealtime(0.5f);
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void InfoUI_toggle()
    {
        UI1.gameObject.SetActive(!UI1.gameObject.activeSelf);
    }
}
