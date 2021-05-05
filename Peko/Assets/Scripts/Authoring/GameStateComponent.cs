using UnityEngine;
using Unity.Entities;

namespace Peko.Authoring
{
    public class GameStateComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new GameState{
                Value = GameStateEnum.Start
            });
        }
    }
}