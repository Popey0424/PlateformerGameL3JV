using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RocketActivation : MonoBehaviour
{

    private Rocket _rocketScript;
    public GameObject[] rockets;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int index = 0;
            foreach (var element in rockets)
            {
                _rocketScript = rockets[index].GetComponent<Rocket>();
                _rocketScript._isActivated = true;
                index++;
            }
        }
    }
}
