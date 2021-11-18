using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LoadText : MonoBehaviour
{
    private UiManager uiManager;

    public GameObject InfoUi;

    //public GameObject UI2;

    //public GameObject Legacy;
    private string TreeTag; //나무 들어갈 이름

    [Range(0.01f, 0.1f)] public float textDelay;//텍스트 표기 속도

    public Text[] Legacy_Text;//표시할 텍스트
    public Text Legacy_Info_text;

    private bool Checking_On_off; //버튼 온오프

    private Coroutine runningCoroutine;

    public RaycastHit hit;//Raycast로 피격한 오브젝트 정보

    float MaxDistance = 300f;//Raycast 사정거리

    public SteamVR_Action_Boolean teleportAction;

    public SteamVR_Input_Sources handType;

    //public GameObject UIComponent;

    //private int layerMask;//Raycast가 식별할 레이어

    //private bool RaycastCheck;//Raycast가 물체를 충돌했는가 체크

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UiManager>();
        //TreePicture = GameObject.Find("TreePicture");
        Checking_On_off = false;
        InfoUi.gameObject.SetActive(false);
        TreeTag = null;
        StartCoroutine(ViewInfo());

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadText_toggle()
    {
        Checking_On_off = !Checking_On_off;


        if (Checking_On_off == true && uiManager.RaycastCheck ==true)
        {
            InfoUI_toggle();

            string txtData = null;
            txtData = GameObject.Find(uiManager.hit.transform.gameObject.name).GetComponent<Text>().text;
            runningCoroutine = StartCoroutine(InputText(txtData));
        }
        else
        {
            if(uiManager.hit.transform.gameObject == null)
            {
                InfoUI_toggle();
                StopCoroutine(runningCoroutine);
                TreeTag = null;
                for(int i=0;i<Legacy_Text.Length;i++)
                {
                    Legacy_Text[i].text = null;
                }
            }
            else if(uiManager.RaycastCheck==false)
            {
                Checking_On_off = !Checking_On_off;
            }
               
        }
        
    }



    public IEnumerator InputText(string txtData)
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
        }*/
    }
    public IEnumerator ViewInfo()
    {
        do
        {
            if (teleportAction.GetState(handType))
            {
                Debug.Log("ViewInfo");
                LoadText_toggle();
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void InfoUI_toggle()
    {
        if (InfoUi.gameObject.activeSelf == true)
        {
            InfoUi.gameObject.SetActive(false);
        }
        else
        {
            InfoUi.gameObject.SetActive(true);
        }
        /*if (UI2.gameObject.activeSelf == true)
        {
            UI2.gameObject.SetActive(false);
        }
        else
        {
            UI2.gameObject.SetActive(true);
        }*/
    }
}
