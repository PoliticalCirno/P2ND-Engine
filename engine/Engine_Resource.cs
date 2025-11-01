using System.Numerics;
using Game_Objects;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Engine_Resource
{
    public enum Shaders //ah fuck this existed? well shit, I should remove this next update
    {
        Mat_PBR_Metallic,
        Mat_Std_Specular,
        Mat_Fullbright
    }
    public class Materials //forgot why i made this
    {
        public Texture2D Albedo { get; set; }
        public Texture2D Normal { get; set; }
        public Texture2D Mrao { get; set; }

        public float albIntensity;
        public float rouIntensity;
        public float metIntensity;
        public float aoIntensity;
        public float emiIntensity;
        public Raylib_cs.Color emissioncolor;

        public Materials(Texture2D albd,Texture2D norm, Texture2D mr, float ai, float ri, float mi, float aoi, float ei)
        {
            Albedo = albd;
            Normal = norm;
            Mrao = mr;
            albIntensity = ai;
            rouIntensity = ri;
            metIntensity = mi;
            aoIntensity = aoi;
            emiIntensity = ei;
        }
    }
    public class Shadercl
    {
        public static Shader Mat_PBR;//Physically based rendering shader. bit overkill.
        public static Shader Mat_STD;//Used to serve a purpose.
        public static Shader Mat_FBR;//Faux based rendering for shading

        public static int emissiveIntensityLoc;
        public static int emissiveColorLoc;
        public static int textureTilingLoc;

        public static unsafe void InitializeShaderPBR()
        {
            //mostly OK
            var usage = 1;
            Mat_PBR = LoadShader("resources/shader/pbr.vs", "resources/shader/pbr.fs");
            Mat_PBR.Locs[(int)ShaderLocationIndex.MapAlbedo] = GetShaderLocation(Mat_PBR, "albedoMap");
            Mat_PBR.Locs[(int)ShaderLocationIndex.MapMetalness] = GetShaderLocation(Mat_PBR, "mraMap");
            Mat_PBR.Locs[(int)ShaderLocationIndex.MapNormal] = GetShaderLocation(Mat_PBR, "normalMap");
            Mat_PBR.Locs[(int)ShaderLocationIndex.MapEmission] = GetShaderLocation(Mat_PBR, "emissiveMap");
            Mat_PBR.Locs[(int)ShaderLocationIndex.ColorDiffuse] = GetShaderLocation(Mat_PBR, "albedoColor");

            //I think I can do with about like 7 light sources.
            Mat_PBR.Locs[(int)ShaderLocationIndex.VectorView] = GetShaderLocation(Mat_PBR, "viewPos");
            var lightCountLoc = GetShaderLocation(Mat_PBR, "numOfLights");
            var maxLightCount = 4;
            SetShaderValue(Mat_PBR, lightCountLoc, &maxLightCount, ShaderUniformDataType.Int); //You are the bane of my existance

            var ambientIntensity = 0.02f;
            var ambientColor = new Color(5, 4, 45, 225);
            var ambientColorNormalized = new Vector3(ambientColor.R / 255.0F, ambientColor.G / 255.0F, ambientColor.B / 255.0F);
            SetShaderValue(Mat_PBR, GetShaderLocation(Mat_PBR, "ambientColor"), &ambientColorNormalized, ShaderUniformDataType.Vec3);
            SetShaderValue(Mat_PBR, GetShaderLocation(Mat_PBR, "ambient"), &ambientIntensity, ShaderUniformDataType.Float);

            emissiveIntensityLoc = GetShaderLocation(Mat_PBR, "emissivePower");
            emissiveColorLoc = GetShaderLocation(Mat_PBR, "emissiveColor");
            textureTilingLoc = GetShaderLocation(Mat_PBR, "tiling"); //apparently I didn't set the default value as .5 by .5

            SetShaderValue(Mat_PBR, GetShaderLocation(Mat_PBR, "useTexAlbedo"), &usage, ShaderUniformDataType.Int);
            SetShaderValue(Mat_PBR, GetShaderLocation(Mat_PBR, "useTexNormal"), &usage, ShaderUniformDataType.Int);
            SetShaderValue(Mat_PBR, GetShaderLocation(Mat_PBR, "useTexMRA"), &usage, ShaderUniformDataType.Int);
            SetShaderValue(Mat_PBR, GetShaderLocation(Mat_PBR, "useTexEmissive"), &usage, ShaderUniformDataType.Int);
        }

        public static unsafe void ShaderUpdateRuntimePrePBR()
        {
            //I forgot what this does // I forgot too ngl
            var Shd_campos = Engine_Logics.Sub.PlayerCon.ControlCorrespondant.camfps.Position;
            SetShaderValue(Mat_PBR, Mat_PBR.Locs[(int)ShaderLocationIndex.VectorView], Shd_campos, ShaderUniformDataType.Vec3);
        }

        public static unsafe void ShaderUpdateRuntimeDuringPBR()
        {
            //THIS SHIT MAKES NO SENSE. I have to call tiling drawcalls for each model drawcalls right 1 frame before model drawcalls, absolutely ridiculous.
            var textile = new Vector2(0.5f, 0.5f);
            SetShaderValue(Mat_PBR, textureTilingLoc, &textile, ShaderUniformDataType.Vec2);
        }

    }
    public class Mdl
    {
        public Model mdl { get; set; } //lod, Having 3 LOD may be the maximum it can handle since I am trying to make it run on a single thread.
        public Model lod1 { get; set; }
        public Model lod2 { get; set; }

        public int ObjId { get; set; } //First One 0x000001
        public int ItemId { get; set; } //Active: 0x000001 and above, non item objects should be relagated to 0x000000 meaning null.

        public Shaders shader { get; set; }
        public List<Materials> matr = new();

        public Mdl(Model mdl, int objId, int itemId)
        {
            this.mdl = mdl;
            ObjId = objId;
            ItemId = itemId;
        }

        public Mdl(Model mdl, Model lod1, int objId, int itemId)
        {
            this.mdl = mdl;
            this.lod1 = lod1;
            ObjId = objId;
            ItemId = itemId;
        }

         public unsafe Mdl(Model mdl, int objId, int itemId, Texture2D albe, Texture2D nm, Shaders shd) //Temporary PBR loading override just to test a basic model.
        {
            this.mdl = mdl;
            ObjId = objId;
            ItemId = itemId;
            SetTextureFilter(albe, TextureFilter.Bilinear);
            SetTextureFilter(nm, TextureFilter.Bilinear);
            this.mdl.Materials[1].Shader = Shadercl.Mat_PBR;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Albedo].Color = Raylib_cs.Color.White;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Metalness].Value = 0.0f;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Roughness].Value = 1.0f;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Occlusion].Value = 1.0f;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Emission].Color = Color.Red;

            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Albedo].Texture = albe;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Normal].Texture = nm;//You are now my 2nd bane of existance.
        }

        public unsafe Mdl(Model mdl, int objId, int itemId, Texture2D albe, Texture2D nm, Texture2D mra, Shaders shd) //Temporary PBR loading override just to test a basic model.
        {
            this.mdl = mdl;
            ObjId = objId;
            ItemId = itemId;
            SetTextureFilter(albe, TextureFilter.Bilinear);
            SetTextureFilter(nm, TextureFilter.Bilinear);
            SetTextureFilter(mra, TextureFilter.Bilinear);
            this.mdl.Materials[1].Shader = Shadercl.Mat_PBR;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Albedo].Color = Raylib_cs.Color.White;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Metalness].Value = 0.00f;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Roughness].Value = 100.0f;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Occlusion].Value = 1.00f;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Emission].Color = Color.Red;

            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Albedo].Texture = albe;
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Normal].Texture = nm;//You are now my 2nd bane of existance.
            this.mdl.Materials[1].Maps[(int)MaterialMapIndex.Metalness].Texture = mra;
        }

        public unsafe Mdl(Model mdl, int objId, int itemId, Texture2D[] albe, Texture2D[] nm, Texture2D[] mrao, Shader shd, Materials mata) //For no-LOD PBR models, for cutscene props or ones i can't bother to make lods.
        {
            this.mdl = mdl;
            ObjId = objId;
            ItemId = itemId;


            for (int i = 0; i <= this.mdl.MaterialCount; i++)
            {

                this.mdl.Materials[i].Shader = shd;
                this.mdl.Materials[i].Maps[(int)MaterialMapIndex.Albedo].Color = Raylib_cs.Color.White;
                this.mdl.Materials[i].Maps[(int)MaterialMapIndex.Metalness].Value = mata.metIntensity;
                this.mdl.Materials[i].Maps[(int)MaterialMapIndex.Roughness].Value = mata.rouIntensity;
                this.mdl.Materials[i].Maps[(int)MaterialMapIndex.Occlusion].Value = mata.aoIntensity;

                this.mdl.Materials[i].Maps[(int)MaterialMapIndex.Albedo].Texture = albe[i];
                this.mdl.Materials[i].Maps[(int)MaterialMapIndex.Normal].Texture = nm[i];
                this.mdl.Materials[i].Maps[(int)MaterialMapIndex.Metalness].Texture = mrao[i];
            }
        }

    }
}
