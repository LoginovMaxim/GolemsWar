using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    [Serializable]
    public static class HexParameters
    {
        public static readonly float InnerRadius = 0.866025f;
        public static readonly float OuterRadius = 1f;
        public static readonly float Height = 0.5f;

        public static readonly List<Vector3> Offsets = new List<Vector3>
        {
            new Vector3(InnerRadius * 2f, 0f, 0f),
            new Vector3(-InnerRadius * 2f, 0f, 0f),
            new Vector3(InnerRadius, 0f, OuterRadius * 1.5f),
            new Vector3(-InnerRadius, 0f, OuterRadius * 1.5f),
            new Vector3(InnerRadius, 0f, -OuterRadius * 1.5f),
            new Vector3(-InnerRadius, 0f, -OuterRadius * 1.5f)
        };

        public static bool CheckDistance(float distance, int view)
        {
            if (distance < 2 * view * Offsets[0].x)
                return true;

            return false;
        }
    }
}
