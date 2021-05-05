using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

namespace Peko
{
    public class CarrotElaseSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<GameState>();
        }
        protected override void OnUpdate ()
        {
            var state = GetSingleton<GameState>();
            if(state.Value != GameStateEnum.InGame)
            {
                return;
            }
            
            var cmdBuffer = new EntityCommandBuffer(Allocator.TempJob);

            Entities
                .WithAll<CarrotTag>()
                .ForEach((Entity entity, int entityInQueryIndex, in Translation trans) =>
                {
                    if(trans.Value.x <= -5)
                    {
                        cmdBuffer.DestroyEntity(entity);
                    }
                }).Run();
            
            cmdBuffer.Playback(EntityManager);
            cmdBuffer.Dispose();
        }
    }
}
