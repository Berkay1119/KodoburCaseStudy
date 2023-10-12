
using UnityEngine;

public class SpawnLocation:MonoBehaviour
{
    private bool _isEmpty=true;

    public bool IsSpawnPointEmpty()
    {
        return _isEmpty;
    }

    public void MakeSpawnPointFull(bool isFull)
    {
        _isEmpty = !isFull;
    }

    
    
}
