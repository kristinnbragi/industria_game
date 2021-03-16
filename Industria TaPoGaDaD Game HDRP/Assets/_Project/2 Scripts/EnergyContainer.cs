﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyContainer : MonoBehaviour {

    public uint energyItTakes;
    public bool on;
    [SerializeField]
    private bool _locked;
    public bool Locked {
        get { return _locked; }
        set {
            _locked = value;
            if (on) {
                On.Invoke();
            } else {
                Off.Invoke();
            }
        }
    }
    Renderer rend;

    public UnityEvent On;
    public UnityEvent Off;

    private AudioClip sfx;
    private AudioClip sfxNegative;
    void Start() {
        rend = GetComponent<Renderer>();
        
        SetColor();

        StartStates();
    }

    void Update() {

    }

    private void Awake()
    {
        sfx = (AudioClip)Resources.Load("Audio/SFX/Button", typeof(AudioClip));
        sfxNegative = (AudioClip)Resources.Load("Audio/SFX/Button Negative", typeof(AudioClip));
        //StartStates();
    }

    public void interact()
    {
        EnergyManager em = FindObjectOfType<EnergyManager>();
        if (on) {
            // If button is on
            em.energy += this.energyItTakes;
            on = false;
            if (!_locked)
            {
                Off.Invoke();
            }
            AudioSource.PlayClipAtPoint(sfx, transform.position);

        } else {
            if (em.energy >= this.energyItTakes) {
                // If button is off and player has enough energy
                em.energy -= this.energyItTakes;
                on = true;
                if (!_locked)
                {
                    On.Invoke();
                }
                AudioSource.PlayClipAtPoint(sfx, transform.position);

            } else {
                // If button is off and player DOES NOT have enough energy
                AudioSource.PlayClipAtPoint(sfxNegative, transform.position, .3f);
            }
        }
        SetColor();
        FindObjectOfType<EnergyMeter>().updateEnergyMeter();
    }
        public void StartStates()
        {
            if (on)
            {
                On.Invoke();
            } else
            {
                Off.Invoke();
            }
        }


        private void SetColor(){
            if (on) {
                rend.material.SetColor("_BaseColor", Color.green);
            }
            else {
                rend.material.SetColor("_BaseColor", Color.red);
            }
        }
    
}
