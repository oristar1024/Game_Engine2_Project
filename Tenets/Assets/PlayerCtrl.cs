using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Transform racketTr;
    public Transform ball;
    public Transform hitPosition;
    bool hit;
    bool canhit;
    float speed = 15f;
    public float force;
    float hitTime = 0.1f;
    float hitCheckTime = 1f;
    float hit_eTime;
    float canhit_eTime;
    Collider ballCollider;
    public int player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (player == 0)
        {
            float h = 0;
            float v = 0;
            if (Input.GetKeyDown(KeyCode.Return) && hit == false)
                hit = true;

            if (Input.GetKey(KeyCode.UpArrow))
                v += 1;
            if (Input.GetKey(KeyCode.DownArrow))
                v -= 1;
            if (Input.GetKey(KeyCode.LeftArrow))
                h -= 1;
            if (Input.GetKey(KeyCode.RightArrow))
                h += 1;
            if (Input.GetKeyDown(KeyCode.L))
                GameManager.instance.Conversion(player);
            Vector3 moveDir = (-Vector3.forward * h) + (Vector3.right * v);
            transform.Translate(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            float h = 0;
            float v = 0;

            if (Input.GetKeyDown(KeyCode.Space) && hit == false)
                hit = true;

            if (Input.GetKey(KeyCode.W))
                v += 1;
            if (Input.GetKey(KeyCode.S))
                v -= 1;
            if (Input.GetKey(KeyCode.A))
                h -= 1;
            if (Input.GetKey(KeyCode.D))
                h += 1;
            if (Input.GetKeyDown(KeyCode.R))
                GameManager.instance.Conversion(player);
            Vector3 moveDir = (Vector3.forward * h) + (-Vector3.right * v);
            transform.Translate(moveDir.normalized * speed * Time.deltaTime);
        }

        if (!hit)
        {
            racketTr.forward = new Vector3(0, racketTr.position.y - ball.position.y, racketTr.position.z - ball.position.z);
            racketTr.Rotate(0, 0, 90);
        }
        else
        {
            if (player == 0)
            {
                if (ball.position.z > transform.position.z)
                    racketTr.Rotate(-600 * Time.deltaTime, 0, 0);
                else
                    racketTr.Rotate(600 * Time.deltaTime, 0, 0);
            }
            else
            {
                if (ball.position.z > transform.position.z)
                    racketTr.Rotate(600 * Time.deltaTime, 0, 0);
                else
                    racketTr.Rotate(-600 * Time.deltaTime, 0, 0);
            }
            hit_eTime += Time.deltaTime;
        }

        if (canhit)
        {
            canhit_eTime += Time.deltaTime;
            if (canhit_eTime > hitCheckTime)
            {
                canhit = false;
                canhit_eTime = 0;
            }
        }

        if(hit_eTime > hitTime)
        {
            hit_eTime = 0;
            hit = false;
        }

        if(hit && canhit)
        {
            Vector3 dir = hitPosition.position - ball.position;
            dir.y = 0;
            ballCollider.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 7, 0);
            GameManager.instance.player_turn = player;
            GameManager.instance.boundCount = 0;
            hit = false;
            canhit = false;
            hit_eTime = 0;
            canhit_eTime = 0;
            GameManager.instance.Clear();
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            canhit = true;
            ballCollider = other;
        }
    }
}
