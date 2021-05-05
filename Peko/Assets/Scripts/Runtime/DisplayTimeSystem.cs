using Unity.Entities;
using Unity.Tiny;

namespace Peko
{
    [UpdateAfter(typeof(GameStartSystem))]
    public class DisplayTimeSystem : SystemBase
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

            var timerEntity = GetSingletonEntity<Timer>();
            var timer = GetComponent<Timer>(timerEntity);

            timer.Value -= Time.DeltaTime;
            if(timer.Value <= 0)
            {
                timer.Value = 0;
                state.Value = GameStateEnum.EndGame;
                var stateEntity = GetSingletonEntity<GameState>();
                EntityManager.SetComponentData<GameState>(stateEntity, state);

                var playerEntity = GetSingletonEntity<Player>();
                var animData = GetComponent<SpriteAnimationData>(playerEntity); 
                animData.AnimType = AnimationType.End;
                animData.ElapsedTime = 0;
                EntityManager.SetComponentData<SpriteAnimationData>(playerEntity, animData);
            }
            EntityManager.SetComponentData<Timer>(timerEntity, timer);

            var buffer = GetBuffer<NumberPlace>(timerEntity);

            var numberEntity = GetSingletonEntity<NumberTag>();
            var number = GetBuffer<NumberSpriteBuffer>(numberEntity);

            var first = buffer[0].Value;
            var second = buffer[1].Value;
            
            int firstNum = (int)(timer.Value % 10);
            int secondNum = (int)(timer.Value % 100 / 10);

            var firstRenderer = GetComponent<SpriteRenderer>(first);
            var secondRenderer = GetComponent<SpriteRenderer>(second);

            firstRenderer.Sprite = number[firstNum].Sprite;
            secondRenderer.Sprite = number[secondNum].Sprite;

            EntityManager.SetComponentData<SpriteRenderer>(first, firstRenderer);
            EntityManager.SetComponentData<SpriteRenderer>(second, secondRenderer);
        }
    }
}
