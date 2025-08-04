using System.Text.Json;
using Engine_Resource;
using Engine_Scenestates;
using static Raylib_cs.Raylib;
using Game_Objects;
using System.Numerics;
using Raylib_cs;

namespace ResourceFile
{

    //IM GONNA CRY || UPDATE: I might not cry.
    public class QueueMaterial
    {
        public string MaterialName { get; set; }
        public string Shader { get; set; } //MAT_PBR, MAT_FBR, Etc etc...
        public string Albedo { get; set; }
        public string Normal { get; set; }
        public string Mrao { get; set; }
        public QueueMaterial(string MaterialName, string Shader, string Albedo, string Normal, string Mrao)
        {
            this.MaterialName = MaterialName;
            this.Shader = Shader;
            this.Albedo = Albedo;
            this.Normal = Normal;
            this.Mrao = Mrao;
        }

    }

    public class LoadQueueResourceMdl
    {
        public required string MdlQ { get; set; }
        // Realistically, I don't think any 1 model will have more than 3 materials at any given time.
        // Infact most would probably use an separate segmented meshes to come together, for any multi-material mdls.
        // I could do this in an array, and not this messy 3 part variable but who else will judge me but Voided himself <3
        // TODO, fix later
        public string MdlMaterial1 { get; set; }
        public string MdlMaterial2 { get; set; }
        public string MdlMaterial3 { get; set; }
        public string MdlLod { get; set; }
        public string MdlLod2 { get; set; }
        public int Objectid { get; set; }
        public int Itemid { get; set; }
        public List<QueueMaterial> Materials = new List<QueueMaterial>();
        public LoadQueueResourceMdl(string MdlQ, string MdlLod, string MdlLod2, int Objectid, int Itemid, string MdlMaterial1, string MdlMaterial2, string MdlMaterial3)
        {
            this.MdlQ = MdlQ;
            this.MdlLod = MdlLod;
            this.MdlLod2 = MdlLod2;
            this.Objectid = Objectid;
            this.Itemid = Itemid;
            this.MdlMaterial1 = MdlMaterial1;
            this.MdlMaterial2 = MdlMaterial2;
            this.MdlMaterial3 = MdlMaterial3;
        }
    }

    public class LoadQueueMapProp
    {
        public int MdlId { get; set; }
        public int PRefId { get; set; }
        public Prop_types Prptyp { get; set; }
        public string PrNameId { get; set; }
        public bool IsInteractable { get; set; }
        public int InteractionId{ get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public float Scale { get; set; }

        public LoadQueueMapProp(int mid, int prid, Prop_types ptype, string pnid, bool intrb, int inid, Vector3 pos, Vector3 rot, float scl)
        {
            MdlId = mid;
            PRefId = prid;
            Prptyp = ptype;
            PrNameId = pnid;
            IsInteractable = intrb;
            InteractionId = inid;
            Position = pos;
            Rotation = rot;
            Scale = scl;
        }
    }

    class LoadingThreads
    {
        public static int CurrentLoad;
        public static List<LoadQueueResourceMdl> MdlQueue = new();
        public static List<LoadQueueMapProp> MapPropQueue = new();
        static void Loading(int i)
        {
            CurrentLoad = i;
            Thread loadSubroutine = new Thread(LoadSubroutine);
            
        }
        //Loading stuff though maybe I should do this asynchronously.

        private static void LoadSubroutine()
        {
            string cacheDir = Directory.GetCurrentDirectory() + @"\resources\req" + $@"\m_cache_part_{CurrentLoad}.fwwvch"; //fuck wrong with val?!
            string mapDir = Directory.GetCurrentDirectory() + @"\resources\map" + $@"\map_sect_{CurrentLoad}.fwwvmp";
            string colDir = Directory.GetCurrentDirectory() + @"\resources\map" + $@"\col_info_{CurrentLoad}.fwwvcl";
            //string matDir = Directory.GetCurrentDirectory() + @"\resources\req" + $@"\mat_cache_part_{CurrentLoad}.fwwvmt"; Embedding the material INTO the model seems more favorable
            string loaded = File.ReadAllText(cacheDir);                                                                      //Time saving wise, but it seems inefficient for any models reusing a material.
            string loadedmp = File.ReadAllText(mapDir);                                                                      //idrc so ehh.
            MdlQueue.Clear();
            MapPropQueue.Clear();
            MdlQueue = JsonSerializer.Deserialize<List<LoadQueueResourceMdl>>(loaded);
            MapPropQueue = JsonSerializer.Deserialize<List<LoadQueueMapProp>>(loadedmp);


            //TODO: Resource caching loop 
            //1. Load the Material file first I think. save them all in the list.
            //2. Load and run a loop that applies the model's texture materials via a crude search I++

            //Everything here is subject for re-write.
            /*for (int i = 0; i < MdlQueue.Count - 1; i++)
            {
                Scenestate.modl.Add(new Mdl(LoadModel(MdlQueue[i].MdlQ), MdlQueue[i].Objectid, MdlQueue[i].Itemid));
            }

            for (int i = 0; i < MapPropQueue.Count - 1; i++)
            {

                for (int n = 0; n < Scenestate.modl.Count - 1; n++)
                {
                    Model submdls;

                    if (Scenestate.modl[n].ObjId == MapPropQueue[i].MdlId)
                    {
                        submdls = Scenestate.modl[n].mdl;
                        Scenestate.prop.Add(new GameObjects.Props(submdls, MapPropQueue[i].PRefId, MapPropQueue[i].Prptyp, MapPropQueue[i].PrNameId, MapPropQueue[i].IsInteractable, MapPropQueue[i].InteractionId, MapPropQueue[i].Position, MapPropQueue[i].Rotation, MapPropQueue[i].Scale));
                        break;//I feel like eating crawfish
                    }

                }
            }*/

        }
    }
}