using UnityEngine;

namespace Helpers
{
    public class LookAtCamera : MonoBehaviour
    {
        public Camera Camera;

        private void Start()
        {
            Camera = Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(Camera.transform.position - transform.position);
        }
    }
}
