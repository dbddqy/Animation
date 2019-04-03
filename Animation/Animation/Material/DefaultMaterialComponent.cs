using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Animation
{
    public class DefaultMaterialComponent : GH_Component
    {
        public DefaultMaterialComponent()
          : base("Default Material", "DM",
              "Define default materials for animation.",
              "Extra", "Animation")
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.tertiary;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "DiffuseColor", GH_ParamAccess.item, System.Drawing.Color.White);
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
            System.Drawing.Color diffuseColor = System.Drawing.Color.Empty;

            DA.GetData(0, ref diffuseColor);

            var material = new Rhino.DocObjects.Material();
            material.DiffuseColor = diffuseColor;

            DA.SetData(0, material);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconDefautMaterial;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a375af48-2e86-46a2-8f35-e998baa6d85b"); }
        }
    }
}