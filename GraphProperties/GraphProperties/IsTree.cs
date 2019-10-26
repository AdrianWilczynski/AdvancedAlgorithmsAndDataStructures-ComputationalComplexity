namespace GraphProperties
{
    public static partial class GraphProperties
    {
        public static bool IsTree(bool isConnected, bool isCyclic)
            => isConnected && !isCyclic;
    }
}