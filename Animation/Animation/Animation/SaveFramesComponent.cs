using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using System.Drawing;

namespace Animation
{
    public class SaveFramesComponent : GH_Component
    {
        public SaveFramesComponent()
          : base("Save Frames", "SF",
              "Save frames captruing rhino viewport.",
              "Extra", "Animation")
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("On", "On", "Button for creating animation frames.", GH_ParamAccess.item, false);
            pManager.AddGeometryParameter("Geometry", "G", "The geometry to be animated.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Material", "M", "The Material of the geometry.", GH_ParamAccess.list);
            pManager.AddTextParameter("Directory", "D", "The directory path where the animation frames are saved", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        private int counter = 0;
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Boolean reset = false;
            DA.GetData(0, ref reset);
            if (!reset)
            {
                this.counter = 0;
                return;
            }

            List<Grasshopper.Kernel.Types.IGH_GeometricGoo> Geometries = new List<Grasshopper.Kernel.Types.IGH_GeometricGoo>();
            GeometryBase geo = null;

            var Materials = new List<Rhino.DocObjects.Material>();
            string directory = string.Empty;

            DA.GetDataList(1, Geometries);
            DA.GetDataList(2, Materials);
            DA.GetData(3, ref directory);

            RhinoDoc doc = RhinoDoc.ActiveDoc;
            int layerIndex = doc.Layers.Add("__temp__", Color.Black);

            int indexGeo = 0, indexMat = 0, n = 0;
            List<Guid> Ids = new List<Guid>();
            bool flag1 = true, flag2 = true;

            while(flag1 || flag2)
            {
                var attr = new Rhino.DocObjects.ObjectAttributes();
                Materials[indexMat].Name = "__temp__" + n.ToString();
                int materialIndex = doc.Materials.Add(Materials[indexMat]);

                attr.LayerIndex = layerIndex;
                attr.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromObject;
                attr.ObjectColor = Materials[indexMat].DiffuseColor;
                attr.MaterialIndex = materialIndex;
                attr.MaterialSource = Rhino.DocObjects.ObjectMaterialSource.MaterialFromObject;

                Guid id;
                if (Geometries[indexGeo].CastTo<GeometryBase>(out geo))
                {
                    id = doc.Objects.Add(geo, attr);
                    Ids.Add(id);
                }
                else
                {
                    Grasshopper.Kernel.Types.GH_Point p = Geometries[indexGeo] as Grasshopper.Kernel.Types.GH_Point;
                    id = doc.Objects.Add(new Rhino.Geometry.Point(p.Value), attr);
                    Ids.Add(id);
                }

                if (indexGeo < Geometries.Count - 1) indexGeo += 1;
                else flag1 = false;
                if (indexMat < Materials.Count - 1) indexMat += 1;
                else flag2 = false;
                n += 1;
            }

            GH_Document ghDoc = Grasshopper.Instances.ActiveCanvas.Document;
            var originalPreviewMode = ghDoc.PreviewMode;
            ghDoc.PreviewMode = GH_PreviewMode.Disabled;
            doc.Views.Redraw();

            Bitmap bitmap = doc.Views.ActiveView.CaptureToBitmap();
            bitmap.Save(directory + counter.ToString("D5") + ".jpg");
            this.counter += 1;

            ghDoc.PreviewMode = originalPreviewMode;

            foreach (var id in Ids) doc.Objects.Delete(id, true);
            for (int i = 0; i < n; i++)
            {
                doc.Materials.DeleteAt(doc.Materials.Count - 1);
            }
            doc.Layers.Delete(layerIndex, true);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconSaveFrames;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6e29f6e5-b6e6-4d11-b1b0-7ad74e3ca5a7"); }
        }
    }
}