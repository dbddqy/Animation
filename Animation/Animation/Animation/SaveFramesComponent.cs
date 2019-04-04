using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using System.Drawing;
using System.IO;
using Grasshopper.Kernel.Parameters;
using Rhino.DocObjects;

namespace Animation
{
    public class SaveFramesComponent : GH_Component
    {
        public SaveFramesComponent()
          : base("Save Frames", "SF","Save frames captruing rhino viewport.","Extra", "Animation")
        {
        }


        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("On", "On", "Button for creating animation frames.", GH_ParamAccess.item, false);
            pManager.AddGeometryParameter("Geometry", "G", "The geometry to be animated.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Material", "M", "The Material of the geometry.", GH_ParamAccess.list);
            pManager.AddParameter(new Param_FilePath(),"Directory", "D", "The directory path where the animation frames are saved", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
        }
        
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool reset = false;
            if(!DA.GetData(0, ref reset))return;
            if (!reset)
            {
                counter = 0;
                return;
            }

            List<Grasshopper.Kernel.Types.IGH_GeometricGoo> geometries = new List<Grasshopper.Kernel.Types.IGH_GeometricGoo>();

            List<Material> materials = new List<Material>();
            string directory = string.Empty;
            if (!DA.GetDataList(1, geometries) && !DA.GetDataList(2, materials) && !DA.GetData(3, ref directory))return;
            RhinoDoc doc = RhinoDoc.ActiveDoc;
            int layerIndex = doc.Layers.Add("__temp__", Color.Black);

            int indexGeo = 0, indexMat = 0, n = 0;
            List<Guid> Ids = new List<Guid>();
            bool flag1 = true, flag2 = true;

            while(flag1 || flag2)
            {
                ObjectAttributes attr = new ObjectAttributes();
                materials[indexMat].Name = "__temp__" + n;
                int materialIndex = doc.Materials.Add(materials[indexMat]);

                attr.LayerIndex = layerIndex;
                attr.ColorSource = ObjectColorSource.ColorFromObject;
                attr.ObjectColor = materials[indexMat].DiffuseColor;
                attr.MaterialIndex = materialIndex;
                attr.MaterialSource = ObjectMaterialSource.MaterialFromObject;

                Guid id;
                if (geometries[indexGeo].CastTo(out GeometryBase geo))
                {
                    id = doc.Objects.Add(geo, attr);
                    Ids.Add(id);
                }
                else
                {
                    Grasshopper.Kernel.Types.GH_Point p = geometries[indexGeo] as Grasshopper.Kernel.Types.GH_Point;
                    id = doc.Objects.Add(new Rhino.Geometry.Point(p.Value), attr);
                    Ids.Add(id);
                }

                if (indexGeo < geometries.Count - 1) indexGeo += 1;
                else flag1 = false;
                if (indexMat < materials.Count - 1) indexMat += 1;
                else flag2 = false;
                n += 1;
            }

            GH_Document ghDoc = Grasshopper.Instances.ActiveCanvas.Document;
            GH_PreviewMode originalPreviewMode = ghDoc.PreviewMode;
            ghDoc.PreviewMode = GH_PreviewMode.Disabled;
            doc.Views.Redraw();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            Bitmap bitmap = doc.Views.ActiveView.CaptureToBitmap();
            bitmap.Save(directory + counter.ToString("D5") + ".jpg");
            counter += 1;

            ghDoc.PreviewMode = originalPreviewMode;

            foreach (Guid id in Ids) doc.Objects.Delete(id, true);
            for (int i = 0; i < n; i++)
                doc.Materials.DeleteAt(doc.Materials.Count - 1);
            doc.Layers.Delete(layerIndex, true);
        }
        private int counter = 0;
        public override GH_Exposure Exposure => GH_Exposure.primary;
        protected override Bitmap Icon => Properties.Resources.iconSaveFrames;
        public override Guid ComponentGuid => new Guid("6e29f6e5-b6e6-4d11-b1b0-7ad74e3ca5a7");
    }
}