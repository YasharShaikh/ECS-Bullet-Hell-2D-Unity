using Unity.Entities;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    private PlayerControls playerControls;


    protected override void OnCreate() // works as Start() in Monobehaviour
    {
        if(!SystemAPI.TryGetSingleton(out InputComponent input))
        {
            EntityManager.CreateEntity(typeof(InputComponent));
        }

        playerControls = new PlayerControls();
        playerControls.Enable();


    }

    protected override void OnUpdate() // works as Update() in Monobehaviour
    {
        Vector2 moveVector = playerControls.Player.Move.ReadValue<Vector2>();
        Vector2 mousePosition = playerControls.Player.MousePos.ReadValue<Vector2>();
        bool shoot = playerControls.Player.Shoot.IsPressed();

        SystemAPI.SetSingleton(new InputComponent
        {
            mousePos = mousePosition,
            movement = moveVector,
            Shoot = shoot
        });

    }
}
