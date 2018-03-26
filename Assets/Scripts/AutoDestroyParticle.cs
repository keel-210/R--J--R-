using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyParticle : MonoBehaviour
{
    void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Destroy(this.gameObject, particleSystem.main.duration);
    }
}
