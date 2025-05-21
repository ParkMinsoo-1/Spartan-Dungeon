using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public PlayerController playerController;
    private float originPlayerSpeed;
    //public ItemData itemData;
    Resource health {get {return resourceManager.health;}}
    Resource stamina {get {return resourceManager.stamina;}}

    private void Start()
    {
        originPlayerSpeed = playerController.Speed;
        //itemData = CharacterManager.Instance.Player._itemData;
    }
    private void Update()
    {
        
        stamina.SubtractionResource(stamina.passiveValue*Time.deltaTime);
        if (stamina.currentValue <= 0f)
        {
            playerController.Speed = 2.0f;
        }
        else if (stamina.currentValue > 0f)
        {
            playerController.Speed = originPlayerSpeed;
        }
    }

    /// <summary>
    /// 코루틴과 리플렉션을 사용하였음. 리플렉션의 경우 챗GPT의 도움을 받았고, 아직 정확하게 구조를 이해하지 못함.
    /// 버프형 아이템의 경우 공격력, 이동속도, 공격속도 등의 다양한 버프가 있기 때문에 습득한 아이템의 ItemData에 따라서
    /// 결정 되도록 하기 위해서 리플렉션이라는 기능을 사용해봤음.
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public IEnumerator BuffCoroutine(ItemData itemData) 
    {
        string methodName = $"Buff_{itemData.buffType}";
        var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            yield return (IEnumerator)method.Invoke(this, new object[] { itemData });
        }
        else
        {
            Debug.Log($"Buff 메서드 {methodName}가 없습니다.");
        }
    }
    public IEnumerator Buff_speed(ItemData item)
    {
        //float originalSpeed = playerController.Speed;
        playerController.Speed += item.buffValue;
        yield return new WaitForSeconds(item.buffDuration);
        playerController.Speed = originPlayerSpeed;
    }
    
}
