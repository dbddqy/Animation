using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Animation
{
    public class ApplyCameraComponent : GH_Component
    {
        public ApplyCameraComponent()
          : base("Apply Camera", "SC",
              "Set animation camera in active viewport",
              "Extra", "Animation")
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.secondary;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("On", "On", "Button for applying the camera setting.", GH_ParamAccess.item);
            pManager.AddGenericParameter("Camera", "C", "Input camera information", GH_ParamAccess.item);
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
            AnimationCamera animationCamera = AnimationCamera.Default;

            DA.GetData(0, ref button);
            DA.GetData(1, ref animationCamera);

            if(button == true)
                animationCamera.SetAimationCamera();
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconApplyCamera;
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