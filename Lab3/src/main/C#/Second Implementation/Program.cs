using System;


namespace TOALab3
{
    class Program
    {
        static void Main()
        {
            HashTable hashTable = new HashTable(2, 50);
            hashTable.Put(new Key("one", 1), 1);
            hashTable.Put(new Key("two", 2), 2);
            hashTable.Put(new Key("three", 3), 3);
            hashTable.Put(new Key("four", 4), 4);
            hashTable.Output();
        }
    }

    class HashTable
    {
        public Node[] storage;
        int capacity;
        int size;
        int occupancy;

        public HashTable(int capacity, int occupancy)
        {
            this.capacity = capacity;
            this.occupancy = occupancy;
            storage = new Node[capacity];
        }

        public void ChangeHashTableCapacity()
        {
            int count = 0;
            int step = 0;
            Node node;
            for (int i = 0; i < storage.Length; i++)
            {
                if (storage[i] != null)
                {
                    count++;
                    
                    if(storage[i].next != null)
                    {
                        node = storage[i].next;
                        while (node != null)
                        {
                            count++;
                            node = node.next;
                        }
                    }
                }
            }
            Node[] oldNodes = new Node[count];
            for (int i = 0; i < storage.Length; i++)
            {
                if (storage[i] != null)
                {
                    oldNodes[step] = new Node(storage[i].value, storage[i].key, null);
                    step++;
                    if (storage[i].next != null)
                    {
                        node = storage[i].next;
                        while (node != null)
                        {
                            oldNodes[step] = new Node(node.value, node.key, null);
                            node = node.next;
                            step++;
                        }
                    }
                }
            }
            size = 0;
            storage = new Node[capacity * 2];
            capacity *= 2;
            for (int i = 0; i < oldNodes.Length; i++)
            {
                Put(oldNodes[i].key, oldNodes[i].value);
            }            
        }

        public bool CheckIfNeedsToChangeCapacity()
        {
            if (size > occupancy * capacity / 100) return true;
            else return false;
        }

        public bool ContainsKey(Key key)
        {
            for (int i = 0; i < storage.Length; i++)
            {
                if (storage[i] != null)
                {
                    Node node = storage[i];
                    while (node != null)
                    {
                        if (node.key.Equals(key)) return true;
                        node = node.next;
                    }
                }
            }
            return false;
        }

        public int GetSize() => size;

        public void Put(Key key, double? value)
        {
            int index = Math.Abs(key.GetHash() % capacity);
            if (ContainsKey(key))
            {
                Remove(key);
                Put(key, value);
                return;
            }
            if (storage[index] == null)
            {
                size++;
                storage[index] = new Node(value, key, null);

            }
            else
            {
                size++;
                Node node = storage[index];
                storage[index] = new Node(value, key, node);

            }
            if (CheckIfNeedsToChangeCapacity()) ChangeHashTableCapacity();
        }

        public double? Get(Key key)
        {
            int index = key.GetHash() % capacity;
            Node node = storage[index];
            while (node != null)
            {
                if (node.key.Equals(key)) return node.value;
                node = node.next;
            }
            return null;
        }

        public void Remove(Key key)
        {
            Node previousNode;
            Node currentNode;
            int index = Math.Abs(key.GetHash() % capacity);
            if (storage[index] == null) return;
            if (storage[index].next == null && key.Equals(storage[index].key))
            {
                size--;
                storage[index] = null;
                return;
            }
            else
            {
                previousNode = storage[index];
                currentNode = previousNode.next;
                while (currentNode != null)
                {
                    if (currentNode.key.Equals(key))
                    {
                        size--;
                        previousNode.next = currentNode.next;
                        return;
                    }
                    else if (previousNode.key.Equals(key))
                    {
                        size--;
                        storage[index] = currentNode;
                        return;
                    }
                    previousNode = currentNode;
                    currentNode = previousNode.next;
                }
            }
        }

        public void Output()
        {
            for (int i = 0; i < storage.Length; i++)
            {
                Node node = storage[i];
                Console.Write($"{i} | ");
                while (node != null)
                {
                    Console.Write($"{node.value} ");
                    node = node.next;
                }
                Console.WriteLine();
            }
        }
    }

    class Node
    {
        public Key key;
        public Node next;
        public double? value;

        public Node(double? value, Key key, Node next)
        {
            this.value = value;
            this.key = key;
            this.next = next;
        }
    }

    class Key
    {
        public string stock;
        public int dayOfYear;

        public Key(string stock, int dayOfYear)
        {
            this.stock = stock;
            this.dayOfYear = dayOfYear;
        }

        public int GetHash()
        {
            int hash = dayOfYear;
            for (int i = 0; i < stock.Length; i++)
            {
                hash += stock[i] * 17;
            }
            return hash;
        }

        public bool Equals(Key other)
        {
            return this.stock == other.stock && this.dayOfYear == other.dayOfYear;
        }
    }
}