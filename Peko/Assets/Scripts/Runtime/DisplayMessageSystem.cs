using Unity.Entities;
using Unity.Tiny;

namespace Peko
{
    public class DisplayMessageSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<GameState>();
        }
        protected override void OnUpdate()
        {
            var state = GetSingleton<GameState>();
            var messageEntity = GetSingletonEntity<MessageTag>();
            var renderer = GetComponent<SpriteRenderer>(messageEntity);
            
            Entity setEntity;
            switch(state.Value)
            {
                case GameStateEnum.Start:
                    setEntity = GetComponent<StartMessage>(messageEntity).Value;
                    break;
                case GameStateEnum.EndGame:
                    setEntity = GetComponent<ContinueMessage>(messageEntity).Value;
                    break;
                default:
                    setEntity = GetComponent<DefaultMessage>(messageEntity).Value;
                    break;
            }

            renderer.Sprite = setEntity;
            EntityManager.SetComponentData<SpriteRenderer>(messageEntity, renderer);
        }
    }
}
