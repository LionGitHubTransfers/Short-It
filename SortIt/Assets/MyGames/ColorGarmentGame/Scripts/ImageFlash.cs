using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFlash : MonoBehaviour
{
    RawImage Img;
    float alpahValue;
    public AnimationCurve curve;
    public float speed;
    [Range(0.0f,1.0f)]
    public float value;

    void Start()
    {
        Img = GetComponent<RawImage>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
           
            
                
                Img.color = new Color(Img.color.r, Img.color.g, Img.color.b, curve.Evaluate(value));
               
            
           
        
        
            
    }
}
