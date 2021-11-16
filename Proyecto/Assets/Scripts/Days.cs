using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Days : MonoBehaviour
{
    [SerializeField] private Vector3 Cicle = new Vector3(1f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Cicle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
