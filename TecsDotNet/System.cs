
namespace TecsDotNet
{
    public abstract class System
    {
        public bool Initialized { get; set; }
        public virtual World World { get; set; }

        public System()
        {
            Initialized = false;
        }

        public virtual void Init()
        {
            Initialized = true;
        }
        
        public virtual void Update(double dt) { }
        
        public virtual void Shutdown() 
        {
            Initialized = false;
        }
    }
}
