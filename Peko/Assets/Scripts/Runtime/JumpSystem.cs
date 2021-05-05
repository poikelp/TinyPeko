using Unity.Entities;
using Unity.Transforms;
using Unity.Tiny.Input;

namespace Peko
{
    public class JumpSystem : SystemBase
    {
        const float SPEED = 1;
        const float GROUND_POS_Y = -2.5f;
        const float GRAVITY = 9.8f;
        const float JUMP_POW = 7.0f;

        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<GameState>();
        }
        protected override void OnUpdate()
        {
            var state = GetSingleton<GameState>();
            

            float dt = Time.DeltaTime;
            bool push = false;
            var Input = World.GetExistingSystem<InputSystem>();
            if(state.Value == GameStateEnum.InGame)
            {
                if(Input.IsTouchSupported() && Input.TouchCount() > 0)
                {
                    push = true;
                }
                if(Input.GetMouseButton(0))
                {
                    push = true;
                }
            }
            

            Entities
                .WithoutBurst()
                .WithAll<Player>()
                .ForEach((ref Translation trans, ref JumpData jump) =>
                {
                    if(trans.Value.y > GROUND_POS_Y)
                    {
                        float newVelocity = jump.Velocity - GRAVITY * dt;
                        jump.Velocity = newVelocity;
                        trans.Value.y = trans.Value.y + newVelocity * dt;
                    }
                    if(trans.Value.y <= GROUND_POS_Y)
                    {
                        if(push)
                        {
                            jump.Velocity = JUMP_POW;
                            trans.Value.y += JUMP_POW * dt;
                        }
                        else
                        {
                            jump.Velocity = 0;
                            trans.Value.y = GROUND_POS_Y;
                        }
                    
                    }
                }).Run();
        }
    }
}
