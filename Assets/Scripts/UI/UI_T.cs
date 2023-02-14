using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Battle.Stage;
using Battle.AI;

namespace UI 
{

}
public class UI_T : MonoBehaviour
{
    // 궁금 사항
    // 플레이어 이미지는 플레이어가 정한대로 저장되서 들어오는 이미지값인가?
    // 플레이어 정보가 저장되는 곳은 어디야
    // 채팅방 구현
    // 

    // 확인할 부분
    // UI -> 코드처리 잘 연결되는지?
    // 각 기능별 함수로 잘 들어오는지?
    // 연결 잘 되었으면 기능 구현 ㄱ

    // 데이터 필요한 부분
    // 플레이어 정보, 라운드 정보, 유닛 카운트 수, 시너지(구현 완) -> 설명 데이터 관리는 따로 필요
    // 시너지 설명 - text, string 2개씩 필요

    // 타이머?
    // 작동 잘됨 -> 포톤 연동했을 때도 잘됐음.

    // 라운드 정보 -> 데이터 어떻게 받아올거야
    // 표시될 text = 6개, int = 2개 필요

    // 유닛 정보 -> 데이터 어떻게 받아올거야
    // 표시될 text = 1개, int = 2개 필요

    // 설정 -> 사운드 적용. 설정창 On / Off 금방 함
    // MASTER - 슬라이더 이미지 Fill 적용, 값 1개 씩 필요
    // BGM - 슬라이더 이미지 Fill 적용, 값 1개 씩 필요
    // EFFECT - 슬라이더 이미지 Fill 적용, 값 1개 씩 필요
    // APPLY 버튼 - OK 버튼? -> 저장기능이 있는건가? -> 어디다 저장해?

    // 시너지, 스킬, 플레이어 이미지 정보 -> 배열 데이터로 저장 필요 -> 갖다 써야됨.

    // 플레이어 HP, 레벨, 닉네임, 골드, 재화 관리 필요 -> 일단 테스트용으로만 제작해서 쓸 것    
    // HP에 따른 랭킹 컨텐츠 이동
    // 상대 플레이어 아이콘을 눌렀을 때 카메라 이동 -> RPG때 만들었던 카메라 참고

    // 시너지 정보에 해당하는 UI를 띄우는 방식
    // 데몬 3마리 -> 데몬 시너지 띄움 -> 어떻게? -> 일단 종류별로 다 만들어놓고 껐다켰다하는 방식
    // 

    // 3D raycast
    // 2D raycast
    // 콜라이더가 있어야 해당 오브젝트의 정보를 불러올 수 있음

    // 상호작용했을 때 정보를 불러와야 하는 UI : 시너지, 스킬, 유닛
    // 상호작용했을 때 정보가 필요 없는 UI : 라운드, 채팅, 세팅, 확장, 뽑기

    // 시너지 컨텐츠 정보와 같은 정보를 가진 팝업 시너지를 띄우려면?
    // 마우스 포지션 위치에 팝업창을 띄워야 함 -> 나중에, 에셋 수정 먼저

    enum Buttons 
    {
       
    }
    enum Texts 
    { 
    
    }
    enum GameObjects 
    { 
    
    }
    enum Images 
    { 
    
    }
    
    private void Start()
    {
        // Bind<Button>(typeof(Buttons));        
        // Bind<Text>(typeof(Buttons));        
        // Bind<GameObject>(typeof(GameObjects));        
        // Bind<Image>(typeof(Images));        
    }
    
    /*protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        objects.Add(typeof(T)),objs);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objs[i] = Util.FindChild(gameObject, names[i], true);
            }
            else 
            {
                objs[i] = Util.FindChild<T>(gameObject, names[i], true);
            }
            if (objs[i]==null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
            
        }
    }*/

   
        
}

/*public class UI_Base 
{
    // Key : type, Value : Object
    protected Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();
    public abstract void Init();
    // Util - 컴포넌트가 있는 모든 자식들 검사
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;
        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null) return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }
}*/

