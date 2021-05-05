using Unity.Entities;
using Unity.Tiny.Input;
using Unity.Collections;

namespace Peko
{
    [UpdateAfter(typeof(DisplayTimeSystem))]
    public class GameResetSystem : SystemBase
    {
        private bool touch = false;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<GameState>();
            RequireSingletonForUpdate<Player>();
        }

        protected override void OnUpdate()
        {
            var state = GetSingleton<GameState>();
            var stateEntity = GetSingletonEntity<GameState>();
            if(state.Value == GameStateEnum.EndGame)
            {
                bool push = false;
                var Input = World.GetExistingSystem<InputSystem>();
                if(Input.IsTouchSupported() && touch && /*Input.TouchCount() > 0*/ Input.GetTouch(0).phase == TouchState.Ended)
                {
                    touch = false;
                    push = true;
                }
                if(Input.GetMouseButtonUp(0))
                {
                    push = true;
                }

                if(Input.IsTouchSupported() && Input.GetTouch(0).phase == TouchState.Began)
                {
                    touch = true;
                }

                if(push)
                {
                    state.Value = GameStateEnum.Start;
                    EntityManager.SetComponentData<GameState>(stateEntity, state);

                    var timerEntity = GetSingletonEntity<Timer>();
                    var timer = GetComponent<Timer>(timerEntity);
                    timer.Value = 60;
                    EntityManager.SetComponentData<Timer>(timerEntity, timer);

                    var counterEntity = GetSingletonEntity<Counter>();
                    var counter = GetComponent<Counter>(counterEntity);
                    counter.Value = 0;
                    EntityManager.SetComponentData<Counter>(counterEntity, counter);

                    var cmdBuffer = new EntityCommandBuffer(Allocator.TempJob);
                    Entities
                        .WithAll<CarrotTag>()
                        .ForEach((Entity entity) =>
                        {
                            cmdBuffer.DestroyEntity(entity);
                        }).Run();
                    cmdBuffer.Playback(EntityManager);
                    cmdBuffer.Dispose();

                    
                    var playerEntity = GetSingletonEntity<Player>();
                    var animData = GetComponent<SpriteAnimationData>(playerEntity); 
                    animData.AnimType = AnimationType.Wait;
                    animData.ElapsedTime = 0;
                    EntityManager.SetComponentData<SpriteAnimationData>(playerEntity, animData);

                    var runSpeed = GetComponent<RunSpeed>(playerEntity);
                    runSpeed.Value = 2.0f;
                    EntityManager.SetComponentData<RunSpeed>(playerEntity, runSpeed);

                    var spawnerEntity = GetSingletonEntity<CarrotSpawner>();
                    var spawner = GetComponent<CarrotSpawner>(spawnerEntity);
                    spawner.TimeLimit = 2.0f;
                    EntityManager.SetComponentData<CarrotSpawner>(spawnerEntity, spawner);
                }
            }
        }
    }
}