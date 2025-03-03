using System.Collections;

using UnityEngine;
using UnityEngine.Pool;

namespace RDong
{
    [DisallowMultipleComponent]
    public class DongGenerator : MonoBehaviour
    {
        [Header("Pool Settings")]

        [SerializeField] bool _collectionChecks;
        [SerializeField] int _maxPoolItem = 50;
        [SerializeField] Dong _dongPrefab;

        [Header("Generator Settings")]

        static DongGenerator _instance;
        ObjectPool<Dong> _pool;

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
            return newDong;
        }

        static void OnReturnToPool(Dong dong) 
        {
            dong.gameObject.SetActive(false);
        }

        static void OnTakeFromPool(Dong dong) 
        {
            dong.gameObject.SetActive(true);
        }

        static void OnDestroyPoolObject(Dong dong) 
        {
            Destroy(dong);
        }

        Vector3 _spos;
        Vector3 _epos;

        float rx_s, rx_e;
        float ry;

        void Awake()
        {
            _instance = this;
            InitializePool();

        }

        void Start()
        {
            rx_s = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height)).x;
            rx_e = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)).x;
            ry = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)).y;
            ry += _dongPrefab.GetComponent<Collider2D>().bounds.size.y;

            StartCoroutine(GenerationRoutine());
        }

        int max_num = 10;

        IEnumerator GenerationRoutine()
        {
            while (true)
            {
                if (Pool.CountActive >= max_num)
                {
                    Debug.Log("Max Dong Limit Reached");
                }
                else
                {
                    Dong d = Pool.Get();

                    float r = Random.Range(rx_s, rx_e);

                    //d.InitializeLifecycle();
                }

                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
