using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCtrl : MonoBehaviour
{
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
}
