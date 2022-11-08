namespace Assets.MobileOptimizedWater.Scripts
{
    using UnityEngine;

    /// <exclude />
    public class AnimationStarter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Motion animation;

        public void Awake()
        {
            animator.Play(animation.name);
        }
    }
}
