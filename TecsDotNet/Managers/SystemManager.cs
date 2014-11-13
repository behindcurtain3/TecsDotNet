using System;
using System.Collections.Generic;

namespace TecsDotNet.Managers
{
    public class SystemEventArgs : EventArgs
    {
        public System System { get; private set; }

        public SystemEventArgs(System s)
        {
            System = s;
        }
    }

    public class SystemManager : List<System>, IManager
    {
        #region Events

        public delegate void SystemEventHandler(object sender, SystemEventArgs e);
        public event SystemEventHandler SystemAdded;
        public event SystemEventHandler SystemRemoved;

        #endregion

        #region Fields

        private List<System> toAdd;
        private List<System> toRemove;

        #endregion

        #region Properties

        public World World { get; private set; }
        
        #endregion

        #region Methods

        public SystemManager(World world)
        {
            World = world;
            toAdd = new List<System>();
            toRemove = new List<System>();
        }

        public new void Add(System s)
        {
            if (s == null)
                return;

            toAdd.Add(s);
            s.World = World;
        }

        public new bool Remove(System s)
        {
            toRemove.Add(s);

            return true;
        }

        public void Update(double dt)
        {
            foreach (System s in this)
                s.Update(dt);

            if (toAdd.Count > 0)
            {
                foreach (System s in toAdd)
                {
                    base.Add(s);

                    s.Init();

                    if (SystemAdded != null)
                        SystemAdded.Invoke(this, new SystemEventArgs(s));
                }

                toAdd.Clear();
            }

            if (toRemove.Count > 0)
            {
                foreach (System s in toRemove)
                {
                    if (base.Remove(s))
                    {
                        s.Shutdown();

                        if (SystemRemoved != null)
                            SystemRemoved.Invoke(this, new SystemEventArgs(s));
                    }
                }

                toRemove.Clear();
            }
        }

        #endregion
    }
}
