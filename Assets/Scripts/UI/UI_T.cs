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
    // �ñ� ����
    // �÷��̾� �̹����� �÷��̾ ���Ѵ�� ����Ǽ� ������ �̹������ΰ�?
    // �÷��̾� ������ ����Ǵ� ���� ����
    // ä�ù� ����
    // 

    // Ȯ���� �κ�
    // UI -> �ڵ�ó�� �� ����Ǵ���?
    // �� ��ɺ� �Լ��� �� ��������?
    // ���� �� �Ǿ����� ��� ���� ��

    // ������ �ʿ��� �κ�
    // �÷��̾� ����, ���� ����, ���� ī��Ʈ ��, �ó���(���� ��) -> ���� ������ ������ ���� �ʿ�
    // �ó��� ���� - text, string 2���� �ʿ�

    // Ÿ�̸�?
    // �۵� �ߵ� -> ���� �������� ���� �ߵ���.

    // ���� ���� -> ������ ��� �޾ƿðž�
    // ǥ�õ� text = 6��, int = 2�� �ʿ�

    // ���� ���� -> ������ ��� �޾ƿðž�
    // ǥ�õ� text = 1��, int = 2�� �ʿ�

    // ���� -> ���� ����. ����â On / Off �ݹ� ��
    // MASTER - �����̴� �̹��� Fill ����, �� 1�� �� �ʿ�
    // BGM - �����̴� �̹��� Fill ����, �� 1�� �� �ʿ�
    // EFFECT - �����̴� �̹��� Fill ����, �� 1�� �� �ʿ�
    // APPLY ��ư - OK ��ư? -> �������� �ִ°ǰ�? -> ���� ������?

    // �ó���, ��ų, �÷��̾� �̹��� ���� -> �迭 �����ͷ� ���� �ʿ� -> ���� ��ߵ�.

    // �÷��̾� HP, ����, �г���, ���, ��ȭ ���� �ʿ� -> �ϴ� �׽�Ʈ�����θ� �����ؼ� �� ��    
    // HP�� ���� ��ŷ ������ �̵�
    // ��� �÷��̾� �������� ������ �� ī�޶� �̵� -> RPG�� ������� ī�޶� ����

    // �ó��� ������ �ش��ϴ� UI�� ���� ���
    // ���� 3���� -> ���� �ó��� ��� -> ���? -> �ϴ� �������� �� �������� �����״��ϴ� ���
    // 

    // 3D raycast
    // 2D raycast
    // �ݶ��̴��� �־�� �ش� ������Ʈ�� ������ �ҷ��� �� ����

    // ��ȣ�ۿ����� �� ������ �ҷ��;� �ϴ� UI : �ó���, ��ų, ����
    // ��ȣ�ۿ����� �� ������ �ʿ� ���� UI : ����, ä��, ����, Ȯ��, �̱�

    // �ó��� ������ ������ ���� ������ ���� �˾� �ó����� ������?
    // ���콺 ������ ��ġ�� �˾�â�� ����� �� -> ���߿�, ���� ���� ����

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
    // Util - ������Ʈ�� �ִ� ��� �ڽĵ� �˻�
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

