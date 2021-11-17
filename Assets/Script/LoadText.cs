using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LoadText : MonoBehaviour
{
    private UiManager uiManger;

    public GameObject UI1;

    public GameObject UI2;

    public GameObject Legacy;
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

    public GameObject UIComponent;

    //private int layerMask;//Raycast가 식별할 레이어

    //private bool RaycastCheck;//Raycast가 물체를 충돌했는가 체크

    // Start is called before the first frame update
    void Start()
    {
        uiManger = GameObject.Find("Controller (right)").GetComponent<UiManager>();
        //TreePicture = GameObject.Find("TreePicture");
        Checking_On_off = false;
        UI1.gameObject.SetActive(false);
        UI2.gameObject.SetActive(false);
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

            //TreeName = hit.transform.name;
        if (Checking_On_off == true && uiManger.RaycastCheck ==true)
        {
            InfoUI_toggle();
            TreeTag = TryingGetTagAtParent(uiManger.hit.transform);

            string txtData = null;
            txtData = UIComponent.transform.Find(hit.transform.name).GetComponent<Text>().text;


            runningCoroutine = StartCoroutine(InputText(txtData));
        }
        else
        {
            if(TreeTag != null)
            {
                InfoUI_toggle();
                StopCoroutine(runningCoroutine);
                TreeTag = null;
                for(int i=0;i<Legacy_Text.Length;i++)
                {
                    Legacy_Text[i].text = null;
                }
            }
            else if(uiManger.RaycastCheck==false)
            {
                Checking_On_off = !Checking_On_off;
            }
               
        }
        
    }

    private string TryingGetTagAtParent(Transform transform)
    {   // 물체의 트랜스폼을 기준으로 태그를 찾고 없다면 부모의 태그를 얻는 함수
        do
        {
            string result = transform.tag.ToString();
            if (result == "Untagged")
            {   // 태그가 없을 경우
                transform = transform.parent;
            }
            else
            {
                return result;
            }
        } while (true);
    }

    public IEnumerator InputText(string txtData)
    {
        Debug.Log(txtData.Length);

        for(int i=0;i<=txtData.Length;i++)
        {
            Legacy_Info_text.text.Substring(0, i);
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
                yield return new WaitForSecondsRealtime(1f);
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void InfoUI_toggle()
    {
        if (UI1.gameObject.activeSelf == true)
        {
            UI1.gameObject.SetActive(false);
        }
        else
        {
            UI1.gameObject.SetActive(true);
        }
        if (UI2.gameObject.activeSelf == true)
        {
            UI2.gameObject.SetActive(false);
        }
        else
        {
            UI2.gameObject.SetActive(true);
        }
    }
}
