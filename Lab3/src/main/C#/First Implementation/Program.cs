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
            Console.WriteLine(hashTable.ContainsValue(4));
            /*Node first = hashTable.first;
            while (first != null)
            {
                Console.WriteLine("F V: " + first.value);
                first = first.nextNode;
            }
            hashTable.Output();
            Console.WriteLine("Size: " + hashTable.GetSize());*/
        }
    }

    class HashTable
    {
        public Node[] storage;
        int capacity;
        int size;
        int occupancy;
        public Node first;
        public Node currentNode;

        public void ChangeHashTableCapacity()
        {
            Node node = first;
            first = null;
            size = 0;
            storage = new Node[capacity * 2];
            capacity *= 2;
            while (node != null)
            {
                Put(node.key, node.value);
                node = node.nextNode;
            }
        }

        public bool ContainsValue(double value)
        {
            Node node = first;
            while (node != null)
            {
                if (node.value == value) return true;
                node = node.nextNode;
            }
            return false;
        }

        public bool CheckIfNeedsToChangeCapacity()
        {
            if (size > occupancy * capacity / 100) return true;
            else return false;
        }

        public HashTable(int capacity, int occupancy)
        {
            this.capacity = capacity;
            this.occupancy = occupancy;
            storage = new Node[capacity];
        }

        public bool ContainsKey(Key key)
        {
            int index = key.GetHash() % capacity;
            Node node = storage[index];
            while (node != null)
            {
                if (node.key.Equals(key)) return true;
                node = node.nextCollision;
            }
            return false;
        }

        public int GetSize() => size;

        public void Put(Key key, double? value)
        {
            int index = Math.Abs(key.GetHash() % capacity);
            if (ContainsKey(key))
            {
                Node node = storage[index];
                storage[index] = new Node(value, key, node.nextCollision, null);
                currentNode.value = value;
                return;
            }
            if (storage[index] == null)
            {
                size++;
                storage[index] = new Node(value, key, null, null);
            }
            else
            {
                size++;
                Node node = storage[index];
                storage[index] = new Node(value, key, node, null);
            }
            if(size == 1)
            {
                first = storage[index];
                currentNode = first;
            }
            else
            {

                currentNode.nextNode = storage[index];
                currentNode = currentNode.nextNode;

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
                node = node.nextCollision;
            }
            return null;
        }

        public void Remove(Key key)
        {
            RemoveFromNextNodesOfFirstNode(key);
            Node previousNode;
            Node currentNode;
            int index = Math.Abs(key.GetHash() % capacity);
            if (storage[index] == null) return;
            if (storage[index].nextCollision == null && key.Equals(storage[index].key))
            {
                size--;
                storage[index] = null;
                return;
            }
            else
            {
                previousNode = storage[index];
                currentNode = previousNode.nextCollision;
                while (currentNode != null)
                {
                    if (currentNode.key.Equals(key))
                    {
                        size--;
                        previousNode.nextCollision = currentNode.nextCollision;
                        return;
                    }
                    else if (previousNode.key.Equals(key))
                    {
                        size--;
                        storage[index] = currentNode;
                        return;
                    }
                    previousNode = currentNode;
                    currentNode = previousNode.nextCollision;
                }
            }
        }

        public void RemoveFromNextNodesOfFirstNode(Key key)
        {
            Node prevNode = first;
            Node currentNode = first.nextNode;
            while (currentNode != null)
            {
                if (currentNode.key.Equals(key))
                {
                    prevNode.nextNode = currentNode.nextNode;
                    return;
                }
                else if (prevNode.key.Equals(key))
                {
                    first = first.nextNode;
                    return;
                }
                prevNode = currentNode;
                currentNode = currentNode.nextNode;
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
                    node = node.nextCollision;
                }
                Console.WriteLine();
            }
        }
    }

    class Node
    {
        public Key key;
        public Node nextNode;
        public double? value;
        public Node nextCollision;

        public Node(double? value, Key key, Node nextCollision, Node nextNode)
        {
            this.value = value;
            this.key = key;
            this.nextCollision = nextCollision;
            this.nextNode = nextNode;
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