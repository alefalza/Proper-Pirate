using UnityEngine;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static Transform[] GetChildren(this Transform t)
        {
            Transform[] children = new Transform[t.childCount];

            for (int i = 0; i < children.Length; i++)
            {
                children[i] = t.GetChild(i);
            }
            
            return children;
        }
    }
}