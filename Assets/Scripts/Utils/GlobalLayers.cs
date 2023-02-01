using UnityEngine;

namespace Utils
{
    public static class MyLayers
    {
        public static string GroundAvailable = "GroundAvailable";
        public static string GroundWithEditableItem = "GroundWithEditableItem";
    }
    public static class GlobalLayers
    {
        public static bool IsGroundAvailable(int layer)
        {
            return LayerMask.NameToLayer(MyLayers.GroundAvailable) == layer;
        }

        public static bool IsGroundWithEditableItem(int layer)
        {
            return LayerMask.NameToLayer(MyLayers.GroundWithEditableItem) == layer;
        }
    }

}
