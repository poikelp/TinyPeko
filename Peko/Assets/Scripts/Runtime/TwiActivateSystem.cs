using Unity.Entities;
using Unity.Tiny;

namespace Peko
{
    public class TwiActivateSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<GameState>();
        }
        protected override void OnUpdate()
        {
            var state = GetSingleton<GameState>();
            var entity = GetSingletonEntity<TwiButtonTag>();
            var renderer = GetComponent<SpriteRenderer>(entity);
            if(state.Value == GameStateEnum.EndGame)
            {
                renderer.Sprite = GetComponent<TwiIcon>(entity).Value;   
            }
            else
            {
                renderer.Sprite = GetSingleton<DefaultMessage>().Value;
            }
            EntityManager.SetComponentData<SpriteRenderer>(entity, renderer);

            
        }
    }
}
