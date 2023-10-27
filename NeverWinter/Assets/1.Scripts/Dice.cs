using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        /*Debug.DrawRay(transform.position, transform.up * 2f, Color.red);
        Debug.DrawRay(transform.position, transform.right * 2f, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 2f, Color.red);
        Debug.DrawRay(transform.position, -transform.up * 2f, Color.red);
        Debug.DrawRay(transform.position, -transform.right * 2f, Color.red);
        Debug.DrawRay(transform.position, -transform.forward * 2f, Color.red);*/

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 2f) && hit.transform.name == "Plane")
        {
            //Debug.Log(6);
        }

        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f) && hit.transform.name == "Plane")
        {
            //Debug.Log(1);
        }

        if (Physics.Raycast(transform.position, transform.right, out hit, 2f) && hit.transform.name == "Plane")
        {
            //Debug.Log(3);
        }

        if (Physics.Raycast(transform.position, -transform.right, out hit, 2f) && hit.transform.name == "Plane")
        {
            //Debug.Log(4);
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f) && hit.transform.name == "Plane")
        {
            //Debug.Log(2);
        }

        if (Physics.Raycast(transform.position, -transform.forward, out hit, 2f) && hit.transform.name == "Plane")
        {
            //Debug.Log(5);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddTorque(new Vector3(
                Random.Range(-180f, 180f),
                Random.Range(-180f, 180f),
                Random.Range(-180f, 180f)));

            rigid.AddForce(new Vector3(0f, 100f, 0f));
        }
    }
}
