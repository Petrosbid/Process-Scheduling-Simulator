namespace operation
{
    internal class MemoryPartition
    {
        public int Size { get; set; }
        public Queue ProcessQueue { get; set; }

        public MemoryPartition(int size)
        {
            Size = size;
            ProcessQueue = new Queue();
        }

    }
}
