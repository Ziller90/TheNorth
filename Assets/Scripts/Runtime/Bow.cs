using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] Animator bowAnimator;
    [SerializeField] Thrower arrowThrower;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject arrowInBow;
    
    public void Pull()
    {
        arrowInBow.SetActive(true);
        bowAnimator.SetTrigger("Pull");
    }

    public void Release()
    {
        arrowInBow.SetActive(false);
        bowAnimator.SetTrigger("Release");
    }
}
