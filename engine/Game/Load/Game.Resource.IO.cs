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

        public static void LoadMaterials()
        {
                string readfile = File.ReadAllText(Directory.GetCurrentDirectory() + "/resources/materials/test/MainTexture.p2mat");
                Console.WriteLine(readfile);
                var matload = JsonSerializer.Deserialize<Game.Resource.Load.Assets.Material>(readfile);

                mat.AddRange(matload);

                Console.WriteLine("0x" + mat[0x000000].MaterialId.ToString("x6"));
        }
        
    }
}
