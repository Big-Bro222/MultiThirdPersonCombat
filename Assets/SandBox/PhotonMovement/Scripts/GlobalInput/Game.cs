using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public static partial class Game
{
    // PRIVATE METHODS

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitializeSubSystem()
    {
        if (Application.isPlaying == false)
            return;

        if (PlayerLoopUtility.HasPlayerLoopSystem(typeof(Game)) == false)
        {
            PlayerLoopUtility.AddPlayerLoopSystem(typeof(Game), typeof(EarlyUpdate), OnEarlyUpdate, 0);
        }

        Application.quitting -= OnApplicationQuit;
        Application.quitting += OnApplicationQuit;

        Debug.LogWarning($"Input system update mode: {InputSystem.settings.updateMode}");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private static void OnEarlyUpdate()
    {
        if (Application.isPlaying == false)
        {
            PlayerLoopUtility.RemovePlayerLoopSystems(typeof(Game));
            return;
        }

        if (InputSystem.settings.updateMode == InputSettings.UpdateMode.ProcessEventsManually)
        {
            //A script just to make sure that the Input system updates before the render part
            InputSystem.Update();
        }

        if (EventSystem.current is InputEventSystem inputEventSystem)
        {
            inputEventSystem.UpdateEventSystem();
        }
    }

    private static void OnApplicationQuit()
    {
        PlayerLoopUtility.RemovePlayerLoopSystems(typeof(Game));
    }
}