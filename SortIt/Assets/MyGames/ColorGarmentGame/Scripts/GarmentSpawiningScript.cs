using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentSpawiningScript : MonoBehaviour
{
    public GameObject[] Garments;
      //pant, underWare, Skirt, Top, socks;
    public int spawnCount = 6;
    Camera mainCam;
    Transform hitObj=null;
    Vector3 originalPos;
    public float NormalValue=150;
    void Start()
    {
        mainCam = Camera.main;
    }

    
    void Update()
    {
        Vector2 mousePos = Input.mousePosition/NormalValue;
        
        //mousePos.z = 2;
        print(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100f);
            Debug.Log("hit " + hit.transform);
            hitObj = hit.transform;
            originalPos = hitObj.position;
           
        }
        if (hitObj != null)
        {
            hitObj.position = new Vector3(mousePos.x,2,mousePos.y);

        }

        


    }
    
}
