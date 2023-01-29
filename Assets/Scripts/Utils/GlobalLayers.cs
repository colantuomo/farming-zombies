using UnityEngine;

namespace Utils
{
    public static class GlobalLayers
    {
        public static bool IsGroundAvailable(int layer)
        {
            return LayerMask.NameToLayer("GroundAvailable") == layer;
        }
    }

}
