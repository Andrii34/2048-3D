using UnityEngine;

[CreateAssetMenu(fileName = "CubeConfigs", menuName = "Scriptable Objects/CubeConfigs")]
public class CubeConfigs : ScriptableObject
{

    [field: SerializeField] public long Points { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    private void OnValidate()
    {

        if (Points <= 2)
        {
            Points = 2;
            return;
        }


        if (!IsPowerOfTwo(Points))
        {

            Points = ClosestPowerOfTwo(Points);
        }
    }

    private bool IsPowerOfTwo(long value)
    {
        return (value & (value - 1)) == 0;
    }

    private long ClosestPowerOfTwo(long value)
    {

        long lower = 2;
        while (lower * 2 <= value)
            lower *= 2;

        long upper = lower * 2;
        if (lower < 2) lower = 2;

        return (value - lower < upper - value) ? lower : upper;
    }
}