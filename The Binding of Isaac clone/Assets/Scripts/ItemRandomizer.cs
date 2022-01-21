using System.Collections.Generic;
using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Рандомайзер пассивных предметов
    /// </summary>
    public class ItemRandomizer : MonoBehaviour
    {
        [SerializeField]
        private Item _defaultItem;
        [SerializeField]
        private List<Item> _itemsWithQuality0;
        [SerializeField]
        private List<Item> _itemsWithQuality1;
        [SerializeField]
        private List<Item> _itemsWithQuality2;
        [SerializeField]
        private List<Item> _itemsWithQuality3;
        [SerializeField]
        private List<Item> _itemsWithQuality4;
        [SerializeField]
        private List<int> _qualityBoundaries;

        private Dictionary<int, List<Item>> _allItems;

        private void Start()
        {
            _allItems = new Dictionary<int, List<Item>>();

            _allItems.Add(0, _itemsWithQuality0);
            _allItems.Add(1, _itemsWithQuality1);
            _allItems.Add(2, _itemsWithQuality2);
            _allItems.Add(3, _itemsWithQuality3);
            _allItems.Add(4, _itemsWithQuality4);
        }

        /// <summary>
        /// Заспавнить рандомный предмет
        /// </summary>
        /// <returns>Предмет</returns>
        public Item SpawnRandomItem()
        {
            Item item = null;

            var currentItemQuality = Random.Range(0, 100);

            for (int i = 0; i < _qualityBoundaries.Count; i++)
            {
                if (currentItemQuality <= _qualityBoundaries[i])
                {
                    if (_allItems[i].Count == 0)
                    {
                        if (i == _qualityBoundaries.Count - 1)
                        {
                            item = ChooseItemDownwards();

                            break;
                        }
                        else
                        {
                            item = ChooseItemUpwards(i);

                            if (item == null)
                            {
                                item = ChooseItemDownwards();
                            }

                            break;
                        }
                    }

                    item = SpawnRandomItemFromPool(_allItems[i]);

                    if (item == null)
                    {
                        continue;
                    }

                    break;
                }
            }

            if (item == null) item = _defaultItem;

            return item;
        }

        /// <summary>
        /// Выбрать предмет из пула
        /// </summary>
        /// <param name="itemPool">Пул</param>
        /// <returns>Предмет</returns>
        public Item SpawnRandomItemFromPool(List<Item> itemPool)
        {
            if (itemPool.Count == 0) return null;

            var itemIndex = Random.Range(0, itemPool.Count);

            var item = itemPool[itemIndex];

            itemPool.RemoveAt(itemIndex);

            return item;
        }

        /// <summary>
        /// Выбрать пердмет от высшего качества вниз
        /// </summary>
        /// <returns>Предмет</returns>
        public Item ChooseItemDownwards()
        { 
            for (int i = _qualityBoundaries.Count - 2; i > -1; i--)
            {
                if (_allItems[i].Count != 0)
                {
                    return SpawnRandomItemFromPool(_allItems[i]);
                }
            }

            return null;
        }

        /// <summary>
        /// Выбрать пердмет от низшего качества вверх
        /// </summary>
        /// <returns>Предмет</returns>
        public Item ChooseItemUpwards(int start)
        {
            for (int i = start + 1; i < _qualityBoundaries.Count; i++)
            {
                if (_allItems[i].Count != 0)
                {
                    return SpawnRandomItemFromPool(_allItems[i]);
                }
            }

            return null;
        }
    }
}