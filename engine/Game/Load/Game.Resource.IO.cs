using System.Text.Json;
using static Raylib_cs.Raylib;
using Engine.Game.Objects;
using System.Numerics;
using Raylib_cs;
using Game.Resource.Load;


namespace Game.Resource.Load.IO
{
    public class Assets
    {
        static List<Game.Resource.Load.Assets.Material> mat = new();
        
        public static void LoadMaterials()
        {
                string readfile = File.ReadAllText(Directory.GetCurrentDirectory() + "/resources/materials/test/MainTexture.p2mat");
                Console.WriteLine(readfile);
                var matload = JsonSerializer.Deserialize<Game.Resource.Load.Assets.Material>(readfile);

                mat.AddRange(matload);

                Console.WriteLine(mat[0].MaterialName);
        }
        
    }
}
