using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MyDragAndDrop : MonoBehaviour
{
    private GameObject target;
    bool isMouseDragging;
    private Vector3 screenPosition;
    private Vector3 offset;
    public Vector3 Offset = new Vector3( 0,3,0);
    public GameObject[] drawers;
    public string[] drawerColors;
   
    

    private void Start()
    {
        drawers = GameObject.FindGameObjectsWithTag("Wardrobe");
        drawerColors = new string[drawers.Length];
        for (int i = 0; i < drawers.Length; i++)
        {
            drawerColors[i] = drawers[i].GetComponent<Wardrobe>().WardrobeColor;
        }
    }
    
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (hit.collider.CompareTag("Garments"))
            {
                targetObject = hit.collider.gameObject;

            }
        }
        return targetObject;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);
            if (target != null)
            {
                isMouseDragging = true;
               // Debug.Log("our target position :" + target.transform.position);
                //Here we Convert world position to screen position.
                screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));

                ///////////////////////////////////////////////////////
                target.GetComponent<GarmentScript>().pickup();

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

        if (isMouseDragging)
        {
            //tracking mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

            //offset.y = target.GetComponent<GarmentScript>().DrawerOriginalPos.y+2.5f;
            //offset.y += 2;
            //It will update target gameobject's current postion.
            //currentPosition.y = target.GetComponent<GarmentScript>().DrawerOriginalPos.y + 2.5f;
            currentPosition.y = 4.0f;
            target.transform.position = currentPosition;
            ///////////////////////////////////////////////////////
            
            
            
           

        }
        else
        {
            if(target!=null)
            target.GetComponent<GarmentScript>().Drop();
            
            target = null;
        }


    }

}
