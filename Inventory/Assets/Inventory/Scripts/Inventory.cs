using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable NotResolvedInText

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        private static readonly Item EmptyItem = new("Empty", new Vector2Int(1, 1));
        
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;
        
        private readonly int _width;
        private readonly int _height;
        private readonly Dictionary<Vector2Int, Item> _itemsMap;
        private readonly Item _emptyItem;
        
        public int Width => _width;
        public int Height => _height;
        
        public int Count
        {
            get
            {
                var set = new HashSet<Item>();
                foreach (var itemPair in _itemsMap)
                {
                    var item = itemPair.Value;
                    if (item != null && !item.Equals(EmptyItem))
                    {
                        set.Add(item);
                    }
                }
        
                return set.Count;
            }
        }

        public Inventory(in int width, in int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            _width = width;
            _height = height;
            _itemsMap = new Dictionary<Vector2Int, Item>();
            
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _itemsMap.TryAdd(new Vector2Int(i, j), EmptyItem);
                }
            }
        }
        
        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            if (items == null) throw new ArgumentNullException();

            foreach (var item in items)
            {
                AddItem(item.Key, item.Value);
            }
        }
        
        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null) throw new ArgumentNullException();
            
            foreach (var item in items)
            {
                AddItem(item);
            }
        }
        
        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null) throw new ArgumentNullException();

            foreach (var item in items)
            {
                AddItem(item.Key, item.Value);
            }
        }
        
        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null) throw new ArgumentNullException();

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        private bool InRange(in Vector2Int position) => _itemsMap.ContainsKey(position);

        private bool IsCorrectSize(in Vector2Int size) => size is { x: > 0, y: > 0 };

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            if (item == null || Contains(item)) return false;
            if (!IsCorrectSize(item.Size)) throw new ArgumentException();
            
            for (int i = 0; i < item.Size.y; i++)
            {
                for (int j = 0; j < item.Size.x; j++)
                {
                    var key = new Vector2Int(position.x + j, position.y + i);
                    if (!InRange(key)) return false;
                    if (IsOccupied(key)) return false;
                }
            }

            return true;
        }

        public bool CanAddItem(in Item item, in int posX, in int posY) => CanAddItem(item, new Vector2Int(posX, posY));

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            if (!CanAddItem(item, position)) return false;

            for (int i = 0; i < item.Size.y; i++)
            {
                for (int j = 0; j < item.Size.x; j++)
                {
                    var key = new Vector2Int(position.x + j, position.y + i);
                    _itemsMap[key] = item;
                }
            }

            OnAdded?.Invoke(item, position);
            return true;
        }

        public bool AddItem(in Item item, in int posX, in int posY) => AddItem(item, new Vector2Int(posX, posY));

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (item is null) return false;
            return !Contains(item) && FindFreePosition(item.Size, out _);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (item == null) return false;

            if (!Contains(item) && FindFreePosition(item.Size, out var freePosition))
            {
                return AddItem(item, freePosition);
            }

            return false;
        }
        
        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            if (!IsCorrectSize(size)) throw new ArgumentOutOfRangeException();
            
            freePosition = default;
            var sizeX = size.x;
            var sizeY = size.y;

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var fits = true;
                    var itemPosition = new Vector2Int(j, i);
                    for (int l = 0; l < sizeY; l++)
                    {
                        for (int m = 0; m < sizeX; m++)
                        {
                            var position = new Vector2Int(itemPosition.x + m, itemPosition.y + l);
                            if (!InRange(position) || IsOccupied(position))
                            {
                                fits = false;
                                break;
                            }
                        }
                    }

                    if (fits)
                    {
                        freePosition = itemPosition;
                        return true;
                    }
                }
            }
            
            // foreach (var itemPair in _itemsMap)
            // {
            //     var fits = true;
            //     var itemPosition = itemPair.Key;
            //     for (int l = 0; l < sizeY; l++)
            //     {
            //         for (int m = 0; m < sizeX; m++)
            //         {
            //             var position = new Vector2Int(itemPosition.x + m, itemPosition.y + l);
            //             if (!InRange(position) || IsOccupied(position))
            //             {
            //                 fits = false;
            //                 break;
            //             }
            //         }
            //     }
            //
            //     if (fits)
            //     {
            //         freePosition = itemPosition;
            //         return true;
            //     }
            // }

            return false;
        }
        
        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            if (item == null) return false;
            
            foreach (var itemInMap in _itemsMap)
            {
                if (item.Equals(itemInMap.Value)) return true;
            }

            return false;
        }
        
        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            if (!InRange(position)) throw new ArgumentOutOfRangeException();

            return !_itemsMap[position].Equals(EmptyItem);
        }
        
        public bool IsOccupied(in int x, in int y) => IsOccupied(new Vector2Int(x, y));
        
        /// <summary>
        /// Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position) => !IsOccupied(position);

        public bool IsFree(in int x, in int y) => IsFree(new Vector2Int(y, x));
        
        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item) => RemoveItem(item, out _);
        
        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = default;
            if (item == null) return false;
            
            var removed = false;
            
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    var itemPosition = new Vector2Int(j, i);
                    var itemInMap = _itemsMap[itemPosition];
                    if (itemInMap.Equals(item))
                    {
                        if (!removed)
                        {
                            position = itemPosition;
                            removed = true;
                        }

                        _itemsMap[itemPosition] = EmptyItem;
                    }
                }
            }

            if (removed) OnRemoved?.Invoke(item, position);

            return removed;
        }
        
        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            if (!InRange(position)) throw new IndexOutOfRangeException();

            var item = _itemsMap[position];
            if (item.Equals(EmptyItem)) throw new NullReferenceException();

            return item;
        }
        
        public Item GetItem(in int x, in int y) => GetItem(new Vector2Int(x, y));
        
        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            item = default;
            if (!InRange(position)) return false;

            var itemInMap = _itemsMap[position];

            if (itemInMap.Equals(EmptyItem)) return false;

            item = itemInMap;
            return true;
        }
        
        public bool TryGetItem(in int x, in int y, out Item item) => 
            TryGetItem(new Vector2Int(x, y), out item);
        
        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (item == null) throw new NullReferenceException();
            
            var positions = new List<Vector2Int>();
            foreach (var itemPair in _itemsMap)
            {
                if (itemPair.Value.Equals(item))
                {
                    positions.Add(itemPair.Key);
                }
            }

            if (positions.Count == 0) throw new KeyNotFoundException();
            
            return positions.ToArray();
        }
        
        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            positions = default;
            if (!Contains(item)) return false;

            positions = GetPositions(item);
            return true;
        }
        
        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            var cleared = false;
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    var key = new Vector2Int(j, i);
                    var item = _itemsMap[key];
                    if (!item.Equals(EmptyItem))
                    {
                        cleared = true;
                        _itemsMap[key] = EmptyItem;
                    }
                }
            }

            if (cleared)
            {
                OnCleared?.Invoke();
            }
        }
        
        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            var itemCells = 0;
            int? itemArea = default;
            foreach (var item in this)
            {
                if (item.Name == name)
                {
                    itemCells++;
                    itemArea ??= item.Size.x * item.Size.y;
                }
            }
        
            if (!itemArea.HasValue || itemArea.Value == 0) return 0;
        
            return itemCells / itemArea.Value;
        }
        
        /// <summary>
        /// Moves a specified item to a target position if it exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int newPosition)
        {
            if (item == null) throw new ArgumentNullException();
            if (!InRange(newPosition) || !Contains(item)) return false;

            for (int i = 0; i < item.Size.y; i++)
            {
                for (int j = 0; j < item.Size.x; j++)
                {
                    var position = new Vector2Int(newPosition.x + j, newPosition.y + i);
                    if (!InRange(position)) return false;
                    var itemInMap = _itemsMap[position];
                    if (!itemInMap.Equals(item) && IsOccupied(position)) return false;
                }
            }

            OnMoved?.Invoke(item, newPosition);
            return true;
        }
        
        /// <summary>
        /// Reorganizes inventory space to make the free area uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            if (Count == 0) return;

            List<Item> allItems = new List<Item>(this.OrderByDescending(x => x.Size.x * x.Size.y).ThenBy(x => x.Name));
            Clear();

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var position = new Vector2Int(j, i);
                    Item addedItem = null;
                    foreach (var itemInBuffer in allItems)
                    {
                        if (CanAddItem(itemInBuffer, position))
                        {
                            addedItem = itemInBuffer;
                            break;
                        }
                    }
                    
                    if (addedItem != null)
                    {
                        AddItem(addedItem, position);
                        allItems.Remove(addedItem);
                        if (allItems.Count == 0) return;
                    }
                }
            }
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var item = _itemsMap[new Vector2Int(i, j)];
                    if (item.Equals(EmptyItem))
                    {
                        matrix[i, j] = null;
                    }
                    else
                    {
                        matrix[i, j] = item;
                    }
                }
            }
        }
        
        public IEnumerator<Item> GetEnumerator()
        {
            var set = new HashSet<Item> { EmptyItem };
            
            foreach (var itemInMap in _itemsMap)
            {
                if (set.Add(itemInMap.Value)) yield return itemInMap.Value;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}