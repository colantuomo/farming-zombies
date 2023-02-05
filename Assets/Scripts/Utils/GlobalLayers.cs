using UnityEngine;

namespace Utils
{
    public static class MyLayers
    {
        public static string GroundAvailable = "GroundAvailable";
        public static string GroundWithEditableItem = "GroundWithEditableItem";
        public static string EditableItem = "EditableItem";
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

        public static bool IsEditableItem(int layer)
        {
            return LayerMask.NameToLayer(MyLayers.EditableItem) == layer;
        }
    }

}
