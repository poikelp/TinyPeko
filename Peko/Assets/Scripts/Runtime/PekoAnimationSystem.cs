using Unity.Entities;
using Unity.Tiny;

namespace Peko
{
    [UpdateAfter(typeof(GameResetSystem))]
    public class PekoAnimationSystem : SystemBase
    {
        private const float timePerSprite = 0.1f;

        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<Player>();
            RequireSingletonForUpdate<GameState>();
        }

        protected override void OnUpdate()
        {

            var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            var ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();

            var character = GetSingletonEntity<Player>();
            var animData = GetComponent<SpriteAnimationData>(character);        

            float elapsedTime = animData.ElapsedTime;
            elapsedTime += Time.DeltaTime;

            if(elapsedTime > timePerSprite)
            {
                
                var renderer = GetComponent<SpriteRenderer>(character);

                Entity animEntity;
                      
                switch(animData.AnimType)
                {
                    case AnimationType.Wait:
                        animEntity = GetSingletonEntity<PlayerWaitAnimTag>();
                        break;
                    case AnimationType.Run:
                        animEntity = GetSingletonEntity<PlayerRunAnimTag>();
                        break;
                    case AnimationType.End:
                        animEntity = GetSingletonEntity<PlayerEndAnimTag>();
                        break;
                    default:
                        animEntity = GetSingletonEntity<PlayerWaitAnimTag>();
                        break;
                }
                var spritesList = GetBuffer<AnimSpritesBuffer>(animEntity);
                
                if(elapsedTime >= timePerSprite*spritesList.Length)
                {
                    elapsedTime = 0;
                }
                var activeSprite = (int)(elapsedTime / timePerSprite);

                renderer.Sprite = spritesList[activeSprite].Sprite;

                EntityManager.SetComponentData<SpriteRenderer>(character, renderer);
            }
            
            animData.ElapsedTime = elapsedTime;
            EntityManager.SetComponentData<SpriteAnimationData>(character, animData);
        }
    }
}
