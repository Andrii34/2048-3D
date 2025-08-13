using UnityEngine;

public class CubeValueGenerator
{
    private const int ValueLow = 2;
    private const int ValueHigh = 4;

    private const float ProbabilityLow = 0.75f; 

    
    public static int GetStartValue()
    {
        float r = Random.value; 
        return r < ProbabilityLow ? ValueLow : ValueHigh;
    }
}
