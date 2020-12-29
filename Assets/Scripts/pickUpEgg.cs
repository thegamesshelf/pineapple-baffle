using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpEgg : MonoBehaviour
{
    [SerializeField] private GameObject eggPickupPS;
    [SerializeField] private GameObject eggControllerGO;
    [SerializeField] AudioSource pickUpEggAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Eggs")
        {
            pickUpEggAudio.PlayOneShot(pickUpEggAudio.clip,.5f);
            eggController.eggCounter += 1;
            GameObject eggPickedUp = Instantiate(eggPickupPS, collision.transform.position, Quaternion.identity) as GameObject;
            eggPickedUp.transform.parent = eggControllerGO.transform;

            Destroy(collision.gameObject);

            eggPickedUp.GetComponent<ParticleSystem>().Play();
            Destroy(eggPickedUp, 10);
        }
    }

}
