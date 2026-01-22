using Raylib_cs;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Constraints;
using BepuUtilities;
using BepuUtilities.Memory;
using System.Numerics;


namespace Game_Physics
{
    class ValPhysics
    {
        public Vector3 Gravity;
        public float LinearDamping;
        public float AngularDamping;
        public static BufferPool bfp = new BufferPool();
        /// <summary>
        //public static Simulation simSpace = Simulation.Create(bfp);
        /// </summary>
        public static void InitializeSpace()
        {
    
        }
    }//Maybe Reuse for wind
}