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
        IObjectPool<Dong> _pool;

        public static DongGenerator Instance => _instance;
        public IObjectPool<Dong> Pool => _pool;

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
            dong.DeinitializeLifecycle();
            dong.gameObject.SetActive(false);
        }

        static void OnTakeFromPool(Dong dong) 
        {
            dong.gameObject.SetActive(true);
            dong.InitializeLifecycle();
        }

        static void OnDestroyPoolObject(Dong dong) 
        {
            Destroy(dong);
        }

        void Awake()
        {
            _instance = this;
            InitializePool();
        }
    }
}
