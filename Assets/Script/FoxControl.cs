using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FoxControl : MonoBehaviour
{
    private Animator Animator;

    private string isRunningParam = "isRunning";
    private string roll = "Rolling";
    private Coroutine coroutine;

    bool isPressed = false;
    void Start()
    {
        Animator = GetComponent<Animator>();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine("Flashlight");
    }

    // Update is called once per frame
    void Update()
    {
        ControlByAnimatorParam();
    }

    IEnumerator Flashlight()
    {
        bool flying = true;
        while (flying == true)
        {
            yield return new WaitUntil(() => isPressed == true); 
        }
    }
    void ControlByAnimatorParam()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Animator.SetBool(isRunningParam, true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Animator.SetBool(isRunningParam, true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Animator.SetBool(isRunningParam, false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Animator.SetBool(isRunningParam, false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animator.SetTrigger(roll);
        }
    }
}
