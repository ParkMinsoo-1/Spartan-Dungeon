using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookItemInfo : MonoBehaviour
{
    public GameObject curInteractGameObject;
    private Interactable interactable;
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public Camera camera;
    public ItemInfoUI itemInfoUI;


    public void Start()
    {
        camera = Camera.main;
    }
    public void Update() //마우스포인트에서 아이템의 정보를 확인함
    {
        if (Time.time - lastCheckTime > checkRate) //업데이트 되는 시간을 조절하기 위함.
        {
            lastCheckTime = Time.time;
            
            //마우스 포인트에서 Ray를 발사함.
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.layer == LayerMask.NameToLayer("Interactable"))
                {
                    if (obj != curInteractGameObject)
                    {
                        curInteractGameObject = obj;
                        interactable = curInteractGameObject.GetComponent<Interactable>();
                        //Debug.Log($"{curInteractGameObject.name} 상호작용 할 수 있습니다.");
                        itemInfoUI.ShowInfo(interactable);
                    }
                }
                else
                {
                    curInteractGameObject = null;
                    interactable = null;
                    itemInfoUI.HideInfo();
                }
            }
            else
            {
                curInteractGameObject = null;
                interactable = null;
                itemInfoUI.HideInfo();
            }
        }
    }
}
