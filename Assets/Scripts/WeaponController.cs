using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Trails;
    private bool isStafe = false;
    private Animator animator;

    public GameObject handWeapon;
    public GameObject backWeapon;

    private bool canAttack = true;

    private void Start() {

        animator = GetComponent<Animator>();
        trailClose();
    }

    void Update()
    {
        animator.SetBool("iS", isStafe);    

        if (Input.GetKeyDown(KeyCode.F) && animator.GetBool("isAttack") == false)
        {
            isStafe = !isStafe;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isStafe && canAttack)
        {
            animator.SetTrigger("Attack");
        }

        if (isStafe == true)
        {
            GetComponent<Controller>().walkType = Controller.MovementType.Strafe;
            GetComponent<IKLook>().Lower();
        }
        if (isStafe == false)
        {
            GetComponent<Controller>().walkType = Controller.MovementType.Directional;
            GetComponent<IKLook>().Boost();
        }
    }

    private void Equip()
    {
        backWeapon.SetActive(false);
        handWeapon.SetActive(true);
    }
    private void Unequip()
    {
        backWeapon.SetActive(true);
        handWeapon.SetActive(false);
    }

    public void trailOpen()
    {
        for (int i = 0; i < Trails.transform.childCount; i++)
        {
            Trails.transform.GetChild(i).gameObject.GetComponent<TrailRenderer>().emitting = true;
        }
    }
    public void trailClose()
    {
        for (int i = 0; i < Trails.transform.childCount; i++)
        {
            Trails.transform.GetChild(i).gameObject.GetComponent<TrailRenderer>().emitting = false;
        }
    }
}
