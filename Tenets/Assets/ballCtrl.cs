using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCtrl : MonoBehaviour
{
    public Stack<Vector3> posStack = new Stack<Vector3>();
    float eTime;
    float saveTime = 0.1f;
    public bool OnConversion;
    bool p1Conversion = true;
    bool p2Conversion = true;
    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "out")
            GameManager.instance.Out();
        if (other.collider.tag == "Net")
        {
            GameManager.instance.boundCount = 0;
            GameManager.instance.Out();
        }
        if(other.collider.tag == "court")
            GameManager.instance.boundCount += 1;

        Debug.Log(other.collider.tag);
    }

    public void Conversion(int player)
    {
        if (player == 0 && p1Conversion)
        {
            OnConversion = true;
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.useGravity = false;
            p1Conversion = false;
        }
        else if(player == 1 && p2Conversion)
        {
            OnConversion = true;
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.useGravity = false;
            p2Conversion = false;
        }
    }

    public void init()
    {
        p1Conversion = true;
        p2Conversion = true;
    }

    public void Clear()
    {
        posStack.Clear();
    }

    private void Update()
    {
        eTime += Time.deltaTime;
        if(eTime > saveTime && OnConversion == false)
        {
            posStack.Push(rigidbody.velocity);
            eTime = 0;
        }
        else if(eTime > saveTime && OnConversion && posStack.Count != 0)
        {
            rigidbody.velocity = -posStack.Pop();
            eTime = 0;
        }
        else if(eTime > saveTime && posStack.Count == 0 && OnConversion)
        {
            OnConversion = false;
            GetComponent<Rigidbody>().useGravity = true;
            eTime = 0;
        }
    }
}
