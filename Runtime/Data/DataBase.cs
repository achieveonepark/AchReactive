using System.Collections.Generic;

namespace GameData
{
    /// <summary>
    /// id → 정의 의 인메모리 인덱스. 모든 도메인이 이 한 클래스를 제네릭으로 공유한다.
    /// 런타임 타입 디스패치(typeof)를 쓰지 않으며, 각 도메인은 자신의 DataBase&lt;T&gt; 인스턴스를
    /// 명시적으로 보유한다.
    /// </summary>
    /// <typeparam name="T">보관할 데이터 타입.</typeparam>
    public sealed class DataBase<T> where T : IData
    {
        private readonly Dictionary<string, T> _map = new Dictionary<string, T>();

        /// <summary>현재 보관 중인 항목 수.</summary>
        public int Count => _map.Count;

        /// <summary>보관 중인 모든 정의.</summary>
        public IReadOnlyCollection<T> All => _map.Values;

        /// <summary>로더로부터 전체를 새로 적재한다(기존 항목은 모두 교체).</summary>
        public void Load(IDataLoader<T> loader)
        {
            _map.Clear();
            Add(loader);
        }

        /// <summary>로더로부터 적재해 병합한다(같은 id 는 덮어씀).</summary>
        public void Add(IDataLoader<T> loader)
        {
            if (loader == null)
                return;

            IReadOnlyList<T> items = loader.Load();
            if (items == null)
                return;

            foreach (T item in items)
                Add(item);
        }

        /// <summary>단일 정의를 추가한다(같은 id 는 덮어씀).</summary>
        public void Add(T item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Id))
                _map[item.Id] = item;
        }

        /// <summary>id 로 조회한다. 없으면 default(T).</summary>
        public T Get(string id)
        {
            if (id != null && _map.TryGetValue(id, out T value))
                return value;
            return default;
        }

        /// <summary>id 로 조회를 시도한다.</summary>
        public bool TryGet(string id, out T value)
        {
            if (id != null)
                return _map.TryGetValue(id, out value);
            value = default;
            return false;
        }

        /// <summary>해당 id 가 존재하는지.</summary>
        public bool Has(string id) => id != null && _map.ContainsKey(id);

        /// <summary>모든 항목을 비운다.</summary>
        public void Clear() => _map.Clear();
    }
}
