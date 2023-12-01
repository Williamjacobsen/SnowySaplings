using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LeaveWorkshop : MonoBehaviour
{
    [SerializeField] private Animator DarkLayerAnimator;

    private bool IsAnimationPlaying = false;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsAnimationPlaying)
            {
                return;
            }

            StartCoroutine(ExitHouse());
        }
    }

    private IEnumerator ExitHouse()
    {
        IsAnimationPlaying = true;

        DarkLayerAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("House");
    }
}
