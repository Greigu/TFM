using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public GameObject playerCam;
    private int itemsGrabbed = 0;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        print(1);
        if (other.gameObject.CompareTag("Player"))
        {
            print(2);
            itemsGrabbed = playerCam.GetComponent<PickItem>().GetKeyItems();
            if(itemsGrabbed < 3)
            {
                print("Bad Ending");
            }
            else
            {
                print("Not that bad Ending");
            }
        }
    }
}
