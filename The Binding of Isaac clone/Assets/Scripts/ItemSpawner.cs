using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Спавнер предметов
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        private Item _item;

        public Item Item { get => _item; set => _item = value; }

        // Start is called before the first frame update
        void Start()
        {
            _item = GameObject.Find("Main Camera").GetComponent<ItemRandomizer>().SpawnRandomItem();
            GetComponent<SpriteRenderer>().sprite = _item.Sprite;
        }
    }
}