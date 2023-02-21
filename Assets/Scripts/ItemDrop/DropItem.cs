//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class DropItem : MonoBehaviour
//{
//    [SerializeField] private DropItem myPrefab;
    
//    Vector3 forcePower;    

//    Rigidbody myRb;


//    private void Start()
//    {       
//        myRb = GetComponent<Rigidbody>();
//        forcePower = new Vector3(Random.Range(-1, 2), 5, Random.Range(-1, 2));
//    }    
//    private void Update()
//    {
//        // 죽었으면 아이템 생성
//        /*if (myMonsterAI.IsDie == true)
//        {
//            Bounce();
//        }*/
//        if (Input.GetMouseButtonDown(0))
//        {
//            Bounce();
//        }
//        if (transform.position.y >= 5f)
//        {            
//            Drop();
//        }
        
//        if (Input.GetMouseButtonDown(1)) 
//        {
//            SceneManager.LoadScene("Droptest");
//        }
//    }

//    private void Bounce()
//    {
//        myPrefab.myRb.useGravity = false;
//        myPrefab.myRb.AddForce(forcePower, ForceMode.Impulse);
//    }
//    private void Drop()
//    {        
//        myPrefab.myRb.useGravity = true;        
//    }
    
//}
