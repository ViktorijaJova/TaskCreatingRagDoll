﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToSiri : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool BIsSiri = false;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!BIsSiri && collision.gameObject.tag == "Projectile")
        {
            ToggleRagdoll(false);
            StartCoroutine(GetBackUp());
        }
    }

    private void ToggleRagdoll(bool bisAnimating)
    {
        BIsSiri = !bisAnimating;
        myCollider.enabled = bisAnimating;
        foreach (Rigidbody ragdollBone in rigidbodies) 
        {
            ragdollBone.isKinematic = bisAnimating;

        }

        GetComponent<Animator>().enabled = bisAnimating;
        if (bisAnimating)
        {
            RandomAnimation();
        }

    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
    }

    void RandomAnimation()
    {
        int randomNum = Random.Range(0, 2);
        Debug.Log(randomNum);
        Animator animator = GetComponent<Animator>();
        if(randomNum == 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }


}
