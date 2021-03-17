using Environment;
using Gameplay;
using UnityEngine;

namespace Helpers
{
    public class FlyCamera : MonoBehaviour
    {
        [Header("Main")]
        public Camera Camera;
        public MapGenerator MapGenerator;
        public float Speed;
        
        [Header("Position")]
        public float PositionDelta;
        public Vector2 LimitPositionX;
        public Vector2 LimitPositionY;
        public Vector2 LimitPositionZ;
        
        [Header("Position")]
        public float RotationDelta;
        public Vector2 LimitRotationX;

        private void Start()
        {
            Camera = GetComponent<Camera>();

            Invoke("SetCameraLimits", 1f);
        }

        private void SetCameraLimits()
        {
            switch (MapGenerator.SquareMapSize)
            {
                case 9:
                    LimitPositionX.x = 2;
                    LimitPositionX.y = 12;
                    LimitPositionZ.x = -6;
                    LimitPositionZ.y = 7;
                    break;
                case 15:
                    LimitPositionX.x = 4;
                    LimitPositionX.y = 20;
                    LimitPositionZ.x = -6;
                    LimitPositionZ.y = 15;
                    break;
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                    FlyByDirection(Vector3.up * PositionDelta);
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                    FlyByDirection(Vector3.down * PositionDelta);
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                    RotationByDirection(Vector3.right * RotationDelta);
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                    RotationByDirection(Vector3.left * RotationDelta);
                
                return;
            }
            
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                FlyByDirection(Vector3.forward * PositionDelta);
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                FlyByDirection(Vector3.back * PositionDelta);
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                FlyByDirection(Vector3.right * PositionDelta);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                FlyByDirection(Vector3.left * PositionDelta);
        }

        public void FlyByDirection(Vector3 target)
        {
            Vector3 targetPosition = transform.position + target;
            
            targetPosition.x = targetPosition.x < LimitPositionX.x ? LimitPositionX.x : targetPosition.x;
            targetPosition.x = targetPosition.x > LimitPositionX.y ? LimitPositionX.y : targetPosition.x;
            
            targetPosition.y = targetPosition.y < LimitPositionY.x ? LimitPositionY.x : targetPosition.y;
            targetPosition.y = targetPosition.y > LimitPositionY.y ? LimitPositionY.y : targetPosition.y;
            
            targetPosition.z = targetPosition.z < LimitPositionZ.x ? LimitPositionZ.x : targetPosition.z;
            targetPosition.z = targetPosition.z > LimitPositionZ.y ? LimitPositionZ.y : targetPosition.z;
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, Speed * Time.deltaTime);
        }
        
        private void RotationByDirection(Vector3 deltaVector)
        {
            Vector3 rotation = transform.eulerAngles + deltaVector;

            rotation.x = rotation.x < LimitRotationX.x ? LimitRotationX.x : rotation.x;
            rotation.x = rotation.x > LimitRotationX.y ? LimitRotationX.y : rotation.x;
            
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
