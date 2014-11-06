using System.Collections.Generic;

namespace TecsDotNet.Managers
{
    public class EntityManager : List<Entity>, IManager
    {
        #region Events

        public delegate void EntityEventHandler(Entity e, World world);
        public event EntityEventHandler EntityAdded;
        public event EntityEventHandler EntityRemoved;

        #endregion
        
        #region Properties

        public World World { get; private set; }

        #endregion

        #region Methods

        public EntityManager(World world)
        {
            World = world;
        }

        public new void Add(Entity e)
        {
            if (e == null)
                return;

            // set the entity id
            e.ID = ++Entity.ID_COUNTER;

            base.Add(e);

            if (EntityAdded != null)
                EntityAdded.Invoke(e, World);
        }

        public new bool Remove(Entity e)
        {
            if (base.Remove(e))
            {
                if (EntityRemoved != null)
                    EntityRemoved.Invoke(e, World);

                return true;
            }

            return false;
        }

        public Entity Get(uint id)
        {
            return Find(delegate(Entity e) { return e.ID == id; });
        }

        #endregion

    }
}
