using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChristmasTreeHouse : MonoBehaviour
{
    [SerializeField] private Animator DarkLayerAnimator;

    private bool IsNearChristmasTree = false;
    private bool IsAnimationPlaying = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        IsNearChristmasTree = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IsNearChristmasTree = false;
    }

    private void Update() 
    {
        if (IsAnimationPlaying)
        {
            return;
        }

        if (IsNearChristmasTree && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(EnterChristmasTreeWorkshop());
        }     
    }

    private IEnumerator EnterChristmasTreeWorkshop()
    {
        IsAnimationPlaying = true;

        DarkLayerAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("ChristmasTreeWorkshop");
    }
}
