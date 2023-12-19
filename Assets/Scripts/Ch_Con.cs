using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ch_Con : MonoBehaviour
{
    public float mSpeed;

    private bool mov;

    private Vector2 input;

    private Animator animator;

    public LayerMask SolidObjectsLayer;
    public LayerMask Interactable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!mov)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
                var tPos = transform.position;

                tPos.x += input.x;
                tPos.y += input.y;

                if (isWalkable(tPos))
                {
                    StartCoroutine(Move(tPos));
                }
            }

        }

        animator.SetBool("mov", mov);
    }

    IEnumerator Move(Vector3 tPos)
    {
        mov = true;
        while ((tPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, tPos, mSpeed * 2 * Time.deltaTime);

            yield return null;
        }
        transform.position = tPos;

        mov = false;
    }

    private bool isWalkable(Vector3 tPos)
    {
        if(Physics2D.OverlapCircle(tPos, 0.05f, SolidObjectsLayer | Interactable) != null)
        {
            return false;
        }

        return true;
    }

}
