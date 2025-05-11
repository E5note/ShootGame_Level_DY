using UnityEngine;

namespace UI
{
    public class Explosion : MonoBehaviour
    {
        private Animator _animation;
        void Start()
        {
            _animation  = GetComponent<Animator>();
            //获得动画时长
            float duration = _animation.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject,duration);
        }
    }
}