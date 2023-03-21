using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRoomConditionsOnCollision : MonoBehaviour
{
    private Material initialMaterial;
    public string headString;
    public RoomController roomController;

    // Start is called before the first frame update
    void Start()
    {
        initialMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == headString)
        {
            roomController.UpdateRoomConditions(this.gameObject);
            GetComponent<Renderer>().material = initialMaterial;
        }
    }
}
