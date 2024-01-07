namespace CompositeQuestsGenerator;

public class NodeVariants
{
    public bool HasNextVariants => _nextVariants.Count > 0;

    private readonly int _index;
    private readonly List<int> _nextVariants;
    private readonly NodeVariants[] _usedVariants;
    private NodeVariants _parentNode;

    public NodeVariants(int nextVariantsCount, int index = 0, NodeVariants parent = null)
    {
        _index = index;
        _parentNode = parent;
        _nextVariants = Enumerable.Range(0, nextVariantsCount).ToHashSet().ToList();
        _usedVariants = new NodeVariants[nextVariantsCount];
    }

    public int GetNextRandomVariant(Random random)
    {
        if (_nextVariants == null || _nextVariants.Count == 0)
            return -1;

        var index = random.Next(0, _nextVariants.Count);
        return _nextVariants[index];
    }

    public void RemoveNextVariant(int variant)
    {
        if (!_nextVariants.Contains(variant))
            return;

        _nextVariants.Remove(variant);
        _usedVariants[variant]?.Dispose();
        _usedVariants[variant] = null;

        if (!HasNextVariants)
        {
            _parentNode?.RemoveNextVariant(_index);
            _parentNode = null;
        }
    }

    public bool TryUseNextVariantsNode(int variant, int nextVariantsCount, out NodeVariants variantsNode)
    {
        if (!_nextVariants.Contains(variant))
        {
            variantsNode = null;
            return false;
        }

        _usedVariants[variant] ??= new NodeVariants(nextVariantsCount, variant, this);
        variantsNode = _usedVariants[variant];
        return true;
    }

    public void Dispose()
    {
        for (var i = 0; i < _usedVariants.Length; i++)
        {
            _usedVariants[i]?.Dispose();
            _usedVariants[i] = null;
        }

        _nextVariants.Clear();
        _parentNode = null;
    }
}