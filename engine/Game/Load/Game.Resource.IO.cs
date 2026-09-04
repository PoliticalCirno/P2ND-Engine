using System.Text.Json;
using static Raylib_cs.Raylib;
using Engine.Game.Objects;
using System.Numerics;
using Raylib_cs;
using Engine.Game.Resource.Load;


namespace Engine.Game.Resource.Load.IO
{
    public class Assets
    {
        public static List<Engine.Game.Resource.Load.Assets.Material> mat = new();
        public static List<Engine.Game.Resource.Load.Assets.Models> models = new();
        public static List<Engine.Game.Resource.Load.Assets.MapCluster> mapCl = new();

        public static void LoadMaterials()
        {
            int dirFileCount = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/resources/materials/", "*.p2mat").Count();
            for(int i = 0; i < dirFileCount; i++)
            {
                string readfile = File.ReadAllText(Directory.GetCurrentDirectory() + $"/resources/materials/{i.ToString("x6")}.p2mat");
                Console.WriteLine(readfile);
                var matload = JsonSerializer.Deserialize<Game.Resource.Load.Assets.Material>(readfile);

                mat.AddRange(matload);

                Console.WriteLine("0x" + mat[0x000000].MaterialId.ToString("x6"));
            }
            Console.WriteLine("\n|| INFO: MATERIALS: All addresses loaded successfully");
        }

        public static void LoadModels()
        {
            int dirFileCount = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/resources/models/", "*.p2mdl").Count();
            for(int i = 0; i < dirFileCount; i++)
            {
                string readfile = File.ReadAllText(Directory.GetCurrentDirectory() + $"/resources/models/{i.ToString("x6")}.p2mdl");
                Console.WriteLine("\n|| MODEL: " + readfile);
                var modload = JsonSerializer.Deserialize<Game.Resource.Load.Assets.Models>(readfile);

                models.AddRange(modload);
            } 
            Console.WriteLine("\n|| INFO: MODELS: All addresses loaded successfully");
        }

        public static void LoadMapClusters()//NOTE, Mapclusters are 200x200 
        {
            int dirFileCount = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/resources/mapclusters/", "*.p2mapcl").Count();
            for(int i = 1; i < dirFileCount; i++)
            {
                string readfile = File.ReadAllText(Directory.GetCurrentDirectory() + $"/resources/models/{i.ToString("x6")}.p2matcl");
                Console.WriteLine("\n|| MAPCLUSTER: " + readfile);
                var mapload = JsonSerializer.Deserialize<Game.Resource.Load.Assets.MapCluster>(readfile);

                mapCl.AddRange(mapload);
            }
        }
        
    }
}
