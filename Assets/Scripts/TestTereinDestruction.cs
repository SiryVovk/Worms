using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActionsMap;

public class TestTereinDestruction : MonoBehaviour, ITestActions
{
    [SerializeField] private Terrein terrein;

    private PlayerActionsMap playerActionsMap;

    private void Awake()
    {
        playerActionsMap = new PlayerActionsMap();
        playerActionsMap.Test.SetCallbacks(this);
    }

    private void OnEnable()
    {
        playerActionsMap.Test.Enable();
    }

    private void OnDisable()
    {
        playerActionsMap.Test.Disable();
    }

    public void OnNewaction(InputAction.CallbackContext context)
    {
        Vector2 screen = context.ReadValue<Vector2>();
        Vector2 world = Camera.main.ScreenToWorldPoint(screen);
        terrein.DestroyTerrain(world, 1f);
    }
}
