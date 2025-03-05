using System.Collections;

using UnityEngine;
using UnityEngine.Pool;

namespace RDong
{
    [DisallowMultipleComponent]
    public class DongGenerator : MonoBehaviour
    {
        [Header("Pool Settings")]

        [SerializeField] bool _collectionChecks = true;
        [SerializeField] int _maxPoolItem = 20;
        [SerializeField] Dong _dongPrefab;

        static int counter = 0;

        [Header("Generator Settings")]

        [SerializeField] int max_num = 10;

        static DongGenerator _instance;
        ObjectPool<Dong> _pool;

        Vector3 _spos, _epos; // 화면 상단 왼~오

        float _rx_s, _rx_e;
        float _ry;

        public static DongGenerator Instance => _instance;
        public ObjectPool<Dong> Pool => _pool;

        void InitializePool()
        {
            _pool = new ObjectPool<Dong>
            (
                createFunc: CreatePooledItem,
                actionOnGet: OnTakeFromPool,
                actionOnRelease: OnReturnToPool,
                actionOnDestroy: OnDestroyPoolObject, 
                collectionCheck:_collectionChecks, 
                defaultCapacity: _maxPoolItem
            );
        }

        Dong CreatePooledItem() 
        {
            Dong newDong = Instantiate(_dongPrefab);

#if UNITY_EDITOR
            newDong.name += $"{counter}";
#endif

            ++counter;
            return newDong;
        }

        static void OnReturnToPool(Dong dong) 
        {
            dong.gameObject.SetActive(false);
            dong.Deinitialize();
        }

        static void OnTakeFromPool(Dong dong) 
        {
            dong.gameObject.SetActive(true);
        }

        static void OnDestroyPoolObject(Dong dong) 
        {
            Destroy(dong);
        }

        void Awake()
        {
            _instance = this;
            InitializePool();

            Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
            Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

            _rx_s = screenBottomLeft.x;
            _rx_e = screenTopRight.x;
            _ry = screenTopRight.y;
            _ry += _dongPrefab.GetComponent<Collider2D>().bounds.size.y;
        }

        public void GenerationStart()
        {
            StartCoroutine(GenerationRoutine());
        }
        public void GenerationEnd()
        {
            StopAllCoroutines();
        }

        IEnumerator GenerationRoutine()
        {
            while (true)
            {
                if (Pool.CountActive >= max_num)
                {
                }
                else
                {
                    Dong d = Pool.Get();

                    // TODO: 생성 로직 퀄업
                    float rx = Random.Range(_rx_s, _rx_e);
                    Vector3 rpos = new(rx, _ry, 0);

                    d.Initialize(rpos);
                }

                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}
