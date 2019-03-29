using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Animation
{
    public class ApplyAnimatinoCameraComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PreviewAnimatinoCameraComponent class.
        /// </summary>
        public ApplyAnimatinoCameraComponent()
          : base("ApplyAnimatinoCamera", "AAC",
              "Apply animation camera in active viewport",
              "Animation", "AnimationCamera")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("AnimationCamera", "AC", "Input camera information", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            AnimationCamera animationCamera = AnimationCamera.Default;
            DA.GetData(0, ref animationCamera);

            animationCamera.SetAimationCamera();
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("61b14d7b-a326-4024-9c6c-bbd1e25a6701"); }
        }
    }
}