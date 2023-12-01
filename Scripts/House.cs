using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// House from outside POV
/// </summary>
public class House : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Animator DarkLayerAnimator;

    private bool IsNearHouse = false;
    private bool IsAnimationPlaying = false;
    private SpriteRenderer _renderer;

    private void Awake() 
    {
        DarkLayerAnimator.SetTrigger("FadeOut");

        _renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IsNearHouse = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IsNearHouse = false;
    }

    private void Update() 
    {
        if (IsAnimationPlaying)
        {
            return;
        }

        if (IsNearHouse && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(EnterHouse());
        }    
    }

    private IEnumerator EnterHouse()
    {
        IsAnimationPlaying = true;
        
        foreach (var sprite in _sprites) 
        {
            _renderer.sprite = sprite;
            yield return new WaitForSeconds(0.2f);
        }

        DarkLayerAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("House");
    }
}
