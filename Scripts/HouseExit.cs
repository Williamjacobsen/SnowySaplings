using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// House from inside POV
/// </summary>
public class HouseExit : MonoBehaviour
{
    [SerializeField] private Animator DarkLayerAnimator;

    private bool IsNearExit = false;
    private bool IsAnimationPlaying = false;

    private void Awake()
    {
        DarkLayerAnimator.SetTrigger("FadeOut");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IsNearExit = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IsNearExit = false;
    }

    private void Update()
    {
        if (IsAnimationPlaying)
        {
            return;
        }

        if (IsNearExit && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ExitHouse());
        }
    }

    private IEnumerator ExitHouse()
    {
        IsAnimationPlaying = true;

        DarkLayerAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Main");
    }
}
