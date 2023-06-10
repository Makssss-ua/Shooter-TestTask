using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingeltonScriptableObject<MasterManager>
{

    [SerializeField]
    private LevelSettings _levelSettings;
    public static LevelSettings LevelSettings { get { return Instance._levelSettings; } }
}
