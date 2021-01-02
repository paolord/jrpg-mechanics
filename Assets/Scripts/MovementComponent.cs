using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private Vector3Int position;

    private void Awake()
    {
        if (position == null)
        {
            position = new Vector3Int(0, 0, 0);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
