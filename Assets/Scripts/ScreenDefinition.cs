using UnityEngine;

namespace RDong
{
    public class ScreenDefinition : MonoBehaviour
    {
        [SerializeField] int _width = 300;
        [SerializeField] int _height = 600;

        static ScreenDefinition _sd;
        static public ScreenDefinition Instance => _sd;

        public int Width => _width;
        public int Height => _height;

        void Awake()
        {
            _sd = this;
        }

#if UNITY_EDITOR
        static Color _rectColor = Color.magenta;

        [UnityEditor.DrawGizmo(UnityEditor.GizmoType.Selected | UnityEditor.GizmoType.NonSelected)]
        static void DrawScreenDefinition(ScreenDefinition t, UnityEditor.GizmoType _)
        {
            Debug.Log("Im Working");
            Gizmos.color = _rectColor;

            float hw = t._width / 2.0f, hh = t._height / 2.0f;

            Vector3 ru = new ( hw,  hh, 0);
            Vector3 rd = new ( hw, -hh, 0);
            Vector3 lu = new (-hw,  hh, 0);
            Vector3 ld = new (-hw, -hh, 0);

            Gizmos.DrawLine(ru, rd);
            Gizmos.DrawLine(rd, ld);
            Gizmos.DrawLine(ld, lu);
            Gizmos.DrawLine(lu, ru);
        }
#endif

    }
}
