using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolParent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public bool IsAttacking { get; set; }

    private void Awake() 
    {
        IsAttacking = false;
    }
    
    private void Update()
    {
        Vector2 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = -(pointerPosition - (Vector2)transform.position).normalized;
    
        transform.up = direction;

        if (!IsAttacking && Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(AttackDelay());
        }
    }

    private IEnumerator AttackDelay()
    {
        IsAttacking = true;
        yield return new WaitForSeconds(0.2f);
        IsAttacking = false;
    }
}
