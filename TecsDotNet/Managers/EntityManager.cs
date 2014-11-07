﻿using System.Collections.Generic;

namespace TecsDotNet.Managers
{
    internal class IdentifierPool
    {
        private uint NextAvailableID;
        private Stack<uint> IDs;

        public IdentifierPool(uint startID = 0)
        {
            NextAvailableID = startID;
            IDs = new Stack<uint>();
        }

        public uint CheckOut()
        {
            if (IDs.Count > 0)
                return IDs.Pop();

            return NextAvailableID++;
        }

        public void CheckIn(uint id)
        {
            IDs.Push(id);
        }
    }

    public class EntityManager : List<Entity>, IManager
    {
        #region Events

        public delegate void EntityEventHandler(Entity e, World world);
        public event EntityEventHandler EntityAdded;
        public event EntityEventHandler EntityRemoved;

        #endregion

        #region Fields

        private IdentifierPool idPool;

        #endregion

        #region Properties

        public World World { get; private set; }

        #endregion

        #region Methods

        public EntityManager(World world)
        {
            World = world;
            idPool = new IdentifierPool();
        }

        public new void Add(Entity e)
        {
            if (e == null)
                return;

            // set the entity id
            e.ID = idPool.CheckOut();

            base.Add(e);

            if (EntityAdded != null)
                EntityAdded.Invoke(e, World);
        }

        public new bool Remove(Entity e)
        {
            if (base.Remove(e))
            {
                idPool.CheckIn(e.ID);

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
