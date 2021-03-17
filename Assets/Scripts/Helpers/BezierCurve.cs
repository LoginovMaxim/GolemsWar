using UnityEngine;

namespace Helpers
{
    public static class BezierCurve
    {
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, float t)
        {
            t = Mathf.Clamp01(t);
            float mT = 1f - t;

            return mT * p0 + t * p1;
        }
        
        public static Vector3 GetDerivative(Vector3 p0, Vector3 p1, float t)
        {
            t = Mathf.Clamp01(t);
            float mT = 1f - t;

            return p1 - p0;
        }
        
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float mT = 1f - t;

            return (mT * mT) * p0 + (2f * t * mT) * p1 + (t * t) * p2;
        }
        
        public static Vector3 GetDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float mT = 1f - t;

            return (2f * t - 2f) * p0 + (2f - 4f * t) * p1 + (2f * t) * p2;
        }
        
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float mT = 1f - t;

            return (mT * mT * mT) * p0 + (3f * t * mT * mT) * p1 + (3f * t * t * mT) * p2 + (t * t * t) * p3;
        }
        
        public static Vector3 GetDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float mT = 1f - t;

            return (3f * mT * mT) * (p1 - p0) + (6f * t * mT) * (p2 - p1) + (3f * t * t) * (p3 - p2);
        }
    }
}