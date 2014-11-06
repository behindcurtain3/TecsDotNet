using TecsDotNet.Managers;

namespace TecsDotNet
{
    public class World
    {
        #region Fields

        private EntityManager entities;
        private SystemManager systems;
        private PrototypeManager prototypes;

        #endregion

        #region Properties

        public EntityManager Entities
        {
            get { return entities; }
        }

        public SystemManager Systems
        {
            get { return systems; }
        }

        public PrototypeManager Prototypes
        {
            get { return prototypes; }
        }

        #endregion

        #region Methods

        public World()
        {
            entities = new EntityManager(this);
            systems = new SystemManager(this);
            prototypes = new PrototypeManager(this);
        }

        public virtual void Update(double dt)
        {
            Systems.Update(dt);
        }

        #endregion
    }
}
