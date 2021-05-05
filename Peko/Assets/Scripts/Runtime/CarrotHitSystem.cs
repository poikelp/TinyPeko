using Unity.Entities;
using Unity.Transforms;
using Unity.U2D.Entities.Physics;
using Unity.Collections;
using Unity.Mathematics;

namespace Peko
{
    public class CarrotHitSystem : SystemBase
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

            var physicsWorldSystem = World.GetExistingSystem<PhysicsWorldSystem>();
            var physicsWorld = physicsWorldSystem.PhysicsWorld;
            var cmdBuffer = new EntityCommandBuffer(Allocator.TempJob);

            var counterEntity = GetSingletonEntity<Counter>();
            var counter = GetComponent<Counter>(counterEntity);

            var spawnerEntity = GetSingletonEntity<CarrotSpawner>();
            var spawner = GetComponent<CarrotSpawner>(spawnerEntity);

            var runSpeedEntity = GetSingletonEntity<RunSpeed>();
            var runSpeed = GetComponent<RunSpeed>(runSpeedEntity);

            Entities
                .WithoutBurst()
                .WithAll<CarrotTag>()
                .ForEach((Entity entity, ref PhysicsColliderBlob collider, ref Translation trans, ref Rotation rot) =>
                {
                    if(physicsWorld.OverlapCollider(
                        new OverlapColliderInput
                        {
                            Collider = collider.Collider,
                            Transform = new PhysicsTransform(trans.Value,rot.Value),
                            Filter = collider.Collider.Value.Filter
                        },
                        out OverlapColliderHit hit
                    ))
                    {
                        cmdBuffer.DestroyEntity(entity);
                        // var index = physicsWorld.AllBodies[hit.PhysicsBodyIndex].Entity.Index;
                        // Debug.Log("hit entity index is " + index);

                        bool hasGold = EntityManager.HasComponent<GoldTag>(entity);
                        if(hasGold)
                        {
                            counter.Value += 4;
                        }

                        counter.Value += 1;
                        spawner.TimeLimit = math.max(spawner.TimeLimit * 0.95f, 0.15f);
                        runSpeed.Value = math.min(runSpeed.Value * (1 / 0.96f), 10.0f);
                    }
                }).Run();
            
            EntityManager.SetComponentData<Counter>(counterEntity, counter);
            EntityManager.SetComponentData<CarrotSpawner>(spawnerEntity, spawner);
            EntityManager.SetComponentData<RunSpeed>(runSpeedEntity, runSpeed);
            
            cmdBuffer.Playback(EntityManager);
            cmdBuffer.Dispose();
        }
    }
}
