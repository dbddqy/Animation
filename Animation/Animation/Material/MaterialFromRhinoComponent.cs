using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Animation
{
    public class MaterialFromRhinoComponent : GH_Component
    {
        public MaterialFromRhinoComponent()
          : base("Material From Rhino", "MfR",
              "Get the material from the current rhino document",
              "Extra", "Animation")
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.tertiary;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "N", "Name of the material", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Material", "M", "Material", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = string.Empty;
            DA.GetData(0, ref name);

            Rhino.RhinoDoc doc = Rhino.RhinoDoc.ActiveDoc;
            int index = doc.Materials.Find(name, true);
            var mat = doc.Materials[index];

            DA.SetData(0, mat);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconMaterialFromRhino;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("88a86d5a-56f6-4c3d-8730-09d43e04aaa8"); }
        }
    }
}