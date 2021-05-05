using UnityEngine;
using Unity.Entities;

namespace Peko.Authoring
{
    public class TwiButtonComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Sprite icon;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<TwiButtonTag>(entity);
            var iconEntity = conversionSystem.GetPrimaryEntity(icon);
            dstManager.AddComponentData<TwiIcon>(entity, new TwiIcon
            {
                Value = iconEntity
            });
        }
    }
    [UpdateInGroup(typeof(GameObjectDeclareReferencedObjectsGroup))]
    class DeclareTwiButtonComponentReference : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((TwiButtonComponent mgr) =>
            {
                if(mgr.icon != null)
                {
                    DeclareReferencedAsset(mgr.icon);
                    
                }                
            });
        }
    }
}