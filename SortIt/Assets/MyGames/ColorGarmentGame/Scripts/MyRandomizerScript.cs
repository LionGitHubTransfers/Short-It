using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRandomizerScript : MonoBehaviour
{
   public Transform[] Children;
   public List<int> RandomList = new List<int>();
   public List<Vector3> PosList = new List<Vector3>();
   ObjectController ObjCont;

    void Start()
    {
        //StartCoroutine(Randdomize());
        //Randdomize();
        //ObjCont = GetComponent<ObjectController>();
            Randdomize();

    }

    // Update is called once per frame
    void Randdomize()
    {
        Children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Children[i] = transform.GetChild(i);
        }
        while (RandomList.Count != Children.Length)
        {
            int x = Random.Range(0, Children.Length);
            //int x;

            while (!RandomList.Contains(x))
            {
                //x = Random.Range(0, Children.Length);
                RandomList.Add(x);

            }


        }
        //while (transform.childCount != 0)
        //{

        //    yield return null;
        //}
        for (int i = 0; i < Children.Length; i++)
        {
            //Children[i].parent = null;
            //PosList.Add(Children[i].position);
            PosList.Add(Children[i].localPosition);

        }

        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].position = PosList[RandomList[i]];
            //Children[i].position = new Vector3(Children[i].position.x, RandomList[i], Children[i].position.z);
           // Debug.Log(RandomList[i] + "......" + PosList[RandomList[i]]);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
        }
    }
}
