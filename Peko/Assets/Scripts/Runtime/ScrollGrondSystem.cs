using Unity.Entities;
using Unity.Transforms;

namespace Peko
{
    public class ScrollGrondSystem : SystemBase
    {
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

            var speed = GetSingleton<RunSpeed>();


            float dt = Time.DeltaTime;
            Entities
                .WithoutBurst()
                .WithAny<GroundTag, CarrotTag>()
                .ForEach((ref Translation trans) =>
                {
                    trans.Value.x = trans.Value.x - Time.DeltaTime * speed.Value;
                    if(trans.Value.x < -20)
                    {
                        trans.Value.x = trans.Value.x + 40;
                    }
                }).Run();
        }
    }
}
