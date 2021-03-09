﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float interactDistance = 5;
    public LayerMask interactLayer;
    public uint energyItTakes;
    public bool inRangeOfButton = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Raycasting
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if (hit.collider.gameObject.tag == "Button")
            {
                energyItTakes = hit.collider.gameObject.GetComponent<EnergyContainer>().energyItTakes;
                FindObjectOfType<RequiredEnergyText>().GetComponent<RequiredEnergyText>().UpdateEnergyMeter();
                inRangeOfButton = true;

                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<EnergyContainer>().interact();
                }
            }
            if (hit.collider.gameObject.tag == "Computer")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.transform.gameObject.GetComponentInParent<CodeLock>().SetValue(hit.transform.name);
                }
            }
        }
        else
        {
            energyItTakes = 0;
            inRangeOfButton = false;
            // Debug.Log("Did not Hit");

        }
    }
}
