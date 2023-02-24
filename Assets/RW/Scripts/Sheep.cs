using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; 
    public float gotHayDestroyDelay; 
    private bool hitByHay; 
    private bool dropped; 

    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody; 
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        hitByHay = true; 
        runSpeed = 0;

        Destroy(gameObject, gotHayDestroyDelay);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Hay") && !hitByHay) 
        {
            Destroy(other.gameObject); 
            HitByHay(); 
        }
        else if (other.CompareTag("DropSheep") && !dropped)
        {
            Drop();
        }
    }

    private void Drop()
    {
        dropped = true;
        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        Destroy(gameObject, dropDestroyDelay); 
    }
}
