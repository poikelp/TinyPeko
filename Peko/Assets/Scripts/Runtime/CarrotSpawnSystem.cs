using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Tiny;

namespace Peko
{
    public class CarrotSpawnSystem : ComponentSystem
    {
        // private float elapsedTime;
        // private float timeLimit = 2.0f;
        private Random random;
        private Random random_gold;

        protected override void OnCreate()
        {
            base.OnCreate();

            RequireSingletonForUpdate<GameState>();

            random = new Random(5000);
            random_gold = new Random(1000);
        }

        protected override void OnUpdate()
        {
            var state = GetSingleton<GameState>();
            if(state.Value != GameStateEnum.InGame)
            {
                return;
            }
            var spawnerEntity = GetSingletonEntity<CarrotSpawner>();
            var spawner = GetSingleton<CarrotSpawner>();
            float elapsedTime = spawner.ElapsedTime;
            float timeLimit = spawner.TimeLimit;

            elapsedTime += Time.DeltaTime;

            if(elapsedTime > timeLimit)
            {
                var y = random.NextFloat(4) - 2;
                var pos = new float3(5,y,0);

                var gold = random_gold.NextFloat(1);
                var renderer = EntityManager.GetComponentData<SpriteRenderer>(spawner.Prefab);
                if(gold <= 0.05)
                {
                    renderer.Sprite = spawner.Gold;
                }
                else
                {
                    renderer.Sprite = spawner.Nomal;
                }
                var carrot = PostUpdateCommands.Instantiate(spawner.Prefab);
                PostUpdateCommands.SetComponent<SpriteRenderer>(carrot, renderer);
                PostUpdateCommands.SetComponent(carrot, new Translation
                {
                    Value = pos
                });
                if(gold <= 0.05)
                {
                    PostUpdateCommands.AddComponent<GoldTag>(carrot);
                }

                elapsedTime = 0;
            
            }
            spawner.ElapsedTime = elapsedTime;
            EntityManager.SetComponentData<CarrotSpawner>(spawnerEntity,spawner);
        
        }
    }
}
