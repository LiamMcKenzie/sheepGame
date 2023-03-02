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

    private SheepSpawner sheepSpawner;

    public float heartOffset; 
    public GameObject heartPrefab; 

    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        GameStateManager.Instance.SavedSheep();
        SoundManager.Instance.PlaySheepHitClip();
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true; 
        runSpeed = 0;

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;


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
        GameStateManager.Instance.DroppedSheep();
        SoundManager.Instance.PlaySheepDroppedClip();
        sheepSpawner.RemoveSheepFromList(gameObject);
        dropped = true;
        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        Destroy(gameObject, dropDestroyDelay); 
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}
