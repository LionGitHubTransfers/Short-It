using DitzeGames.Effects;
using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentScript : MonoBehaviour
{
    public string GarmentType;
    public string GarmentColor;
    public Material Red, Yellow, Green, Blue, Purple, Pink, Orange;
    public GameObject particleeffect;
    CameraEffects cameffects;
    public float clothDropSpeed = 5f;

    public GameObject RealCoth;
    //public GameObject ClothColider;
    ObiParticleAttachment clothAttachment;
    Vector3 OriginalPos; 
    Quaternion  originalRotation;
    ObiCloth obiCloth;
    GameObject[] wardeobes;
    string[] wardrobeColors;
    Transform matchedDrawer = null;
    [HideInInspector] public Vector3 DrawerOriginalPos;
    MyDragAndDrop dragScript;
    public bool slideDrawer =false;
    GameObject[] Garments;
    ProgressBar progressBar;
    public Animator flashAnim;
    List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
    List<string> _gColorList = new List<string>();
    List<string> _gTypeList = new List<string>();
    List<int> myX;
    int x;
    public GameObject ObiPrefab;
    public List<GameObject> ThisGarmentTypeList = new List<GameObject>();
    AudioManager audioManager;
    void Start()
    {
        GameObject realClothPref = Instantiate(ObiPrefab, transform.position, Quaternion.identity);
        realClothPref.transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.y);
        RealCoth = realClothPref.transform.GetChild(0).gameObject;
        RealCoth.GetComponent<ObiParticleAttachment>().target = transform;
        flashAnim = GameObject.FindObjectOfType<ImageFlash>().GetComponent<Animator>();

        Garments = GameObject.FindGameObjectsWithTag("Garments");

        PickMat();
        clothAttachment = RealCoth.GetComponent<ObiParticleAttachment>();
        obiCloth = RealCoth.GetComponent<ObiCloth>();
        ///RealCoth.SetActive(false);
        obiCloth.enabled = false;

        RealCoth.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        //ClothColider.GetComponent<MeshRenderer>().enabled = false;
        RealCoth.transform.position = transform.position;
        
        OriginalPos = transform.position;
        originalRotation= transform.rotation;
        GetComponent<ObiCollider>().enabled = false;

        cameffects = Camera.main.GetComponent<CameraEffects>();
        progressBar = GameObject.FindObjectOfType<ProgressBar>();
        progressBar.maxValue = Garments.Length;
        clothDropSpeed = 12f;
        audioManager = FindObjectOfType<AudioManager>();

    }



    void PickMat()
    {


        wardeobes = GameObject.FindGameObjectsWithTag("Wardrobe");
        wardrobeColors = new string[wardeobes.Length];
        for (int i = 0; i < wardeobes.Length; i++)
        {
            wardrobeColors[i] = wardeobes[i].GetComponent<Wardrobe>().WardrobeColor;
            //print(wardeobes[i].GetComponent<Wardrobe>().WardrobeColor);
        }

        
       // x = Random.Range(0, wardrobeColors.Length);
       // GarmentColor = wardrobeColors[x];

        for (int i = 0; i < Garments.Length; i++)
        {
            if(Garments[i].GetComponent<GarmentScript>().GarmentType == GarmentType)
            {
                ThisGarmentTypeList.Add(Garments[i]);
            }
        }
        foreach (var item in ThisGarmentTypeList)
        {
            item.GetComponent<GarmentScript>().GarmentColor = wardrobeColors[ThisGarmentTypeList.IndexOf(item)];
            
        }
        
        switch (GarmentColor)
        {
            case "Red":
                GetComponent<Renderer>().material = Red;
                
                break;
            case "Yellow":
                GetComponent<Renderer>().material = Yellow;
                
                break;
            case "Green":
                GetComponent<Renderer>().material = Green;
                break;
            
            case "Orange":
                GetComponent<Renderer>().material = Orange;
               
                break;
            case "Blue":
                GetComponent<Renderer>().material = Blue;
               

                break;
            case "Purple":
                GetComponent<Renderer>().material = Purple;
              

                break;
            case "Pink":
                GetComponent<Renderer>().material = Pink;
               

                break;
        }

        


    }
    public void Drop()
    {
        if (properDrobe)
        {
            
          StartCoroutine(ToCenter());


        }
        else
        {
            audioManager.PlaySound("Wrong");
           progressBar.WrongBox();
          cameffects.Shake();
          flashAnim.Play("Imageflash");
          StartCoroutine(ResetGarment());
        }
    }
    public void pickup()
    {
        audioManager.PlaySound("PickUp");
       // audioManager.PlaySound("Woosh");

        OriginalPos = transform.position;
        GetComponent<MeshRenderer>().enabled = false;
        RealCoth.transform.position = transform.position;
        //RealCoth.SetActive(true);
        obiCloth.enabled = true;
            if (slideDrawer)
            StartCoroutine(DrawerSlide(true));

    }
    IEnumerator ToCenter()
    {


        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100f))
        {
            if (hit.transform.CompareTag("obst"))
            {
                Ypos *= hit.transform.childCount;
                while (transform.position != new Vector3(drobeobj.position.x, transform.position.y, drobeobj.position.z))
                {
                    
                    
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(drobeobj.position.x, drobeobj.position.y+Ypos, drobeobj.position.z),
                        Time.deltaTime*clothDropSpeed
                        );
                    

                    yield return null;
                }

                audioManager.PlaySound("Drop");

                //transform.position = new Vector3(drobeobj.position.x, hit.point.y + .1f, drobeobj.position.z);
                transform.rotation = drobeobj.rotation;
                transform.parent = hit.transform;

               GameObject Pfx= Instantiate(particleeffect, transform.position, Quaternion.identity);

                Destroy(Pfx.gameObject, 2.5f);

                obiCloth.enabled = false;
                
                GetComponent<MeshRenderer>().enabled = true;
                progressBar.Value++;


                
            }
            else
            {
                StartCoroutine(ResetGarment());
            }


        }


        
    }
    float Ypos = 0.2f;
    IEnumerator DrawerSlide(bool Outside)
    {
        

        if (matchedDrawer == null)
        {
            
            for (int i = 0; i < wardrobeColors.Length; i++)
            {

                if (wardrobeColors[i] == GarmentColor)
                {

                    
                    matchedDrawer = wardeobes[i].transform;
                    DrawerOriginalPos = matchedDrawer.position;
                    //dragScript.Offset.y = OriginalPos.y + 2;
                    

                    break;
                }

            }
        }

        if (Outside)
        {
            Vector3 newPos = new Vector3(matchedDrawer.position.x, matchedDrawer.position.y, 7f);
            while (matchedDrawer.position != newPos)
            {
                matchedDrawer.position = Vector3.MoveTowards(matchedDrawer.position, newPos, (Time.deltaTime * 20.5f));

                yield return null;
            }
        }
        else
        {
            Vector3 newPos = DrawerOriginalPos;
            while (matchedDrawer.position != newPos)
            {
                matchedDrawer.position = Vector3.MoveTowards(matchedDrawer.position, newPos, (Time.deltaTime * 25.5f));

                yield return null;
            }
            matchedDrawer = null;
        }
            
           
        
    }
    public IEnumerator ResetGarment()
    {
        transform.rotation = originalRotation;
        audioManager.PlaySound("Woosh");

        while (transform.position != OriginalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, OriginalPos,(Time.deltaTime* clothDropSpeed));
            yield return null;
        }
        if(transform.position==OriginalPos)
        {
            audioManager.PlaySound("Drop");

            GetComponent<MeshRenderer>().enabled = true;
            transform.position = OriginalPos;

            //RealCoth.transform.parent.position = transform.position;
            RealCoth.transform.position = transform.position;
            //RealCoth.SetActive(false);
            obiCloth.enabled = false;
        }
        //print("Reseting");
        //while(transform.position != OriginalPos)
        //{
        //    yield return null;
        //}
      


    }
   
    bool properDrobe;
    Transform drobeobj;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wardrobe"))
        {
            if(other.GetComponent<Wardrobe>().WardrobeColor == GarmentColor)
            {
                properDrobe = true;
                drobeobj = other.transform;
                //transform.rotation = drobeobj.rotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, drobeobj.rotation, Mathf.Clamp((Time.deltaTime *3), 0, 1.0f));
            }
            else
            {
                properDrobe = false;
                drobeobj = null;
            }
                
        }
    }
    IEnumerator mactchRealcloth()
    {
        clothAttachment.enabled = false;
        // RealCloth rc = RealCoth.GetComponent<RealCloth>();

        while (Input.GetKeyDown(KeyCode.C)!=true)
        {
            yield return null;
        }
        
        //yield return new WaitForSeconds(2f);
        Instantiate(particleeffect, transform.position, Quaternion.identity);
        //transform.rotation = RealCoth.transform.rotation;
        GetComponent<MeshRenderer>().enabled = true;
        //RealCoth.SetActive(false);
        obiCloth.enabled = false;
        //rc.HitObst = false;
        progressBar.Value++;


        if (slideDrawer)
        {
            yield return new WaitForSeconds(.7f);
            StartCoroutine(DrawerSlide(false));
        }
       

    }
}
