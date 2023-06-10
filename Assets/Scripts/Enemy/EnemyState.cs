using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : ScriptableObject
{
    public bool isFinished { get; protected set; }
    public virtual void Init() { }
    public virtual void Init(NavMeshAgent agent, EnemyPlayerDetector playerDetector, EnemySizeContoller sizeContoller, Animator anim) { }
    public virtual void Init(NavMeshAgent agent, Vector3 center, EnemyHealth enemyHealth) { }
    public abstract void Run();
}
