using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Peko;

namespace Peko.Authoring{
    public class CarrotSpawnerComponent : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public GameObject Prefab = null;
        public Sprite gold;
        public Sprite nomal;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new CarrotSpawner{
                Prefab = conversionSystem.GetPrimaryEntity(Prefab),
                ElapsedTime = 0,
                TimeLimit = 2.0f,
                Gold = conversionSystem.GetPrimaryEntity(gold),
                Nomal = conversionSystem.GetPrimaryEntity(nomal)
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            if(Prefab != null)
            {
                referencedPrefabs.Add(Prefab);
            }
        }
    }
    [UpdateInGroup(typeof(GameObjectDeclareReferencedObjectsGroup))]
    class DeclareCarrotComponentReference : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((CarrotSpawnerComponent mgr) =>
            {
                if(mgr.gold != null)
                {
                    DeclareReferencedAsset(mgr.gold);
                }          
                if(mgr.nomal != null)      
                {
                    DeclareReferencedAsset(mgr.nomal);
                }
            });
        }
    }
}