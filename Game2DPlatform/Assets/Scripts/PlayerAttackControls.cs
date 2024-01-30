using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControls : MonoBehaviour
{
    private PlayerMoveControls pMC;
    private GatherInput gI;
    private Animator anim;

    public bool attackStarted = false;
    public PolygonCollider2D polyCol;

    // Start is called before the first frame update
    void Start()
    {
        pMC = GetComponent<PlayerMoveControls>();
        gI = GetComponent<GatherInput>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (gI.tryAttack)
        {
            if (attackStarted || pMC.hasControl == false || pMC.knockBack)
                return;

            anim.SetBool("Attack", true);
            attackStarted = true;
        }
    }

    public void ActivateAttack()
    {
        polyCol.enabled = true;
    }

    public void ResetAttack()
    {
        anim.SetBool("Attack", false);
        gI.tryAttack = false;
        attackStarted = false;
        polyCol.enabled = false;
    }
}
