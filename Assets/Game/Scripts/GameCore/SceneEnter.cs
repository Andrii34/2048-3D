using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class SceneEnter : MonoBehaviour
{
     public abstract  UniTask InitializeAsync();
}
