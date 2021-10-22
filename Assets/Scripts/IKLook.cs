using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{
    private float Weight = 1;
    private Animator animator;
    private Camera mainCam;

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
    } 

    private void OnAnimatorIK(int layerIndex) 
    {
        animator.SetLookAtWeight(Weight, .2f, 1.2f, .5f, .5f);

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);
        animator.SetLookAtPosition(lookAtRay.GetPoint(25));
    }

    public void Boost(){
        Weight = Mathf.Lerp(Weight, 1f, Time.fixedDeltaTime);
    }

    public void Lower(){
        Weight = Mathf.Lerp(Weight, 0, Time.fixedDeltaTime);
    }
}
