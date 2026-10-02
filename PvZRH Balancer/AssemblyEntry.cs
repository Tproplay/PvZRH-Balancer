#if MELONLOADER
using MelonLoader;
using PvZRH_Balancer;

[assembly: MelonInfo(typeof(MelonLoaderEntry), "Pvz Fusion Balancer", "4.0", "Tproplay")]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace PvZRH_Balancer;

public class MelonLoaderEntry : MelonMod
{
    public override void OnInitializeMelon()
    {
        Main.Initialize();
    }

    public override void OnUpdate() => Main.Instance?.OnUpdate();
}
#elif BEPINEX
using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace PvZRH_Balancer;

[BepInPlugin("com.tproplay.balancer", "Pvz Fusion Balancer", "4.0")]
public class BepInExEntry : BasePlugin
{
    public override void Load()
    {
        Main.Initialize();

        ClassInjector.RegisterTypeInIl2Cpp<BalancerModUnityHook>();
        AddComponent<BalancerModUnityHook>();
    }
}

public class BalancerModUnityHook : MonoBehaviour
{
    public BalancerModUnityHook(IntPtr ptr) : base(ptr) { }

    private void Update() => Main.Instance?.OnUpdate();
}
#endif