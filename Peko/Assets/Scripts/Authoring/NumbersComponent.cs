using UnityEngine;
using Unity.Entities;
using Peko;

namespace Peko.Authoring
{
    public class NumbersComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Sprite[] sprites;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var spritesBuffer = dstManager.AddBuffer<NumberSpriteBuffer>(entity);
            if(sprites != null)
            {
                foreach(var s in sprites)
                {
                    var spriteEntity = conversionSystem.GetPrimaryEntity(s);
                    spritesBuffer.Add(new NumberSpriteBuffer
                    {
                        Sprite = spriteEntity
                    });    
                }    
            }

            dstManager.AddComponent<NumberTag>(entity);
        }
    }

    [UpdateInGroup(typeof(GameObjectDeclareReferencedObjectsGroup))]
    class DeclareNumberComponentReference : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((NumbersComponent mgr) =>
            {
                if(mgr.sprites != null)
                {
                    foreach(var s in mgr.sprites)
                    {
                        DeclareReferencedAsset(s);
                    }
                }                
            });
        }
    }
}