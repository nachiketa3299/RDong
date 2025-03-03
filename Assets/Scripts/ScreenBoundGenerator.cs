using UnityEngine;

namespace RDong
{
    [DisallowMultipleComponent]
    public class ScreenBoundGenerator : MonoBehaviour
    {
        [SerializeField] float _thickness = 1.0f;
        
        private enum Dir { L = 0, R, T, B }
        
        Vector2 _bndMin, _bndMax;
        BoxCollider2D[] _bcs = new BoxCollider2D[4];

        void Awake()
        {
            // 카메라 기준 화면 바운더리 계산
            _bndMin = Camera.main.ScreenToWorldPoint(Vector2.zero);
            _bndMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            CreateBound(Dir.L, new Vector2(_bndMin.x - _thickness * 0.5f, (_bndMin.y + _bndMax.y) * 0.5f),
                        new Vector2(_thickness, _bndMax.y - _bndMin.y + _thickness * 2));

            CreateBound(Dir.R, new Vector2(_bndMax.x + _thickness * 0.5f, (_bndMin.y + _bndMax.y) * 0.5f),
                        new Vector2(_thickness, _bndMax.y - _bndMin.y + _thickness * 2));


            //CreateBound(Dir.T, new Vector2((_bndMin.x + _bndMax.x) * 0.5f, _bndMax.y + _thickness * 0.5f),
            //            new Vector2(_bndMax.x - _bndMin.x + _thickness * 2, _thickness));

            CreateBound(Dir.B, new Vector2((_bndMin.x + _bndMax.x) * 0.5f, _bndMin.y - _thickness * 0.5f),
                        new Vector2(_bndMax.x - _bndMin.x + _thickness * 2, _thickness));

            _bcs[(uint)Dir.L].gameObject.layer = LayerMask.NameToLayer("Wall");
            _bcs[(uint)Dir.R].gameObject.layer = LayerMask.NameToLayer("Wall");
            _bcs[(uint)Dir.B].gameObject.layer = LayerMask.NameToLayer("DDongKill");
        }

        void CreateBound(Dir dir, Vector2 position, Vector2 size)
        {
            GameObject go = new GameObject($"Bound_{dir}");
            go.transform.SetParent(transform);
            go.transform.position = position;

            BoxCollider2D bc = go.AddComponent<BoxCollider2D>();
            bc.size = size;

            _bcs[(int)dir] = bc;
        }
    }
}
