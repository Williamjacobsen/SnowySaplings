using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // todo: add sprint

    private readonly float moveSpeed = 2.5f;

    private float moveHorizontal;
    private float moveVertical;

    private SpriteRenderer _renderer;
    private Animator _animator;

    private void Awake() 
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        CameraFollowPlayer();
    }

    private void Update() 
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() 
    {
        HandleSpriteState();
        MovePlayer();
    }

    private void HandleSpriteState()
    {
        if (_renderer.flipX && moveHorizontal > 0)
        {
            _renderer.flipX = false;
        }
        else if (!_renderer.flipX && moveHorizontal < 0)
        {
            _renderer.flipX = true;
        }

        _animator.SetBool("Horizontal", moveHorizontal != 0);
        _animator.SetFloat("Vertical", moveVertical);
    }

    private void MovePlayer()
    {
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            transform.position += new Vector3(moveHorizontal, moveVertical) * moveSpeed * Time.deltaTime;
            CameraFollowPlayer();
        }
    }

    private void CameraFollowPlayer()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}
