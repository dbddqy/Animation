using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Animation
{
    public class BakeMaterialComponent : GH_Component
    {
        public BakeMaterialComponent()
          : base("Bake Material", "BM",
              "Bake the render material to the current rhino document",
              "Extra", "Animation")
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.tertiary;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("On", "On", "Button for baking the Material.", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "The name of the material.", GH_ParamAccess.item, "unknown material");
            pManager.AddGenericParameter("Material", "M", "The Material to be baked into the current rhino document.", GH_ParamAccess.item);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Boolean button = false;
            String name = "";
            var material = new Rhino.DocObjects.Material();

            DA.GetData(0, ref button);
            DA.GetData(1, ref name);
            DA.GetData(2, ref material);

            if (button == true)
            {
                material.Name = name;
                Rhino.RhinoDoc.ActiveDoc.RenderMaterials.Add(material.RenderMaterial);
            }
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconBakeMaterial;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e0b3b8bb-7920-41be-bec9-66fa2e3ffa65"); }
        }
    }
}