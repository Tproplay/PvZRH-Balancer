namespace PvZRH_Balancer;

public class Main
{
    public static Main? Instance { get; private set; }
    public static void Initialize()
    {
        Instance = new Main();
    }

    public void OnUpdate()
    {

    }
}
