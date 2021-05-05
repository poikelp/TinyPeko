using Unity.Entities;
using Unity.U2D.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Peko{
    public class ScrollBGSystem : SystemBase
    {
        const float SPEED = 1;

        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<GameState>();
        }
        protected override void OnUpdate()
        {
            var state = GetSingleton<GameState>();
            if(state.Value != GameStateEnum.InGame)
            {
                return;
            }

            float dt = Time.DeltaTime;
            Entities
                .WithoutBurst()
                .WithAll<BGTag>()
                .ForEach((ref Translation trans) =>
                {
                    trans.Value.x = trans.Value.x - Time.DeltaTime * SPEED;
                    if(trans.Value.x < -20)
                    {
                        trans.Value.x = trans.Value.x + 40;
                    }
                }).Run();
        }
    }
}

