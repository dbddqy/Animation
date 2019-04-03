using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Animation
{
    public class CameraComponent : GH_Component
    {
        public CameraComponent()
          : base("Camera", "C",
              "Define cameras for animation",
              "Extra", "Animation")
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.secondary;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Location", "L", "Define the location of the camera", GH_ParamAccess.item);
            pManager.AddPointParameter("Target", "T", "Define the target of the camera", GH_ParamAccess.item);
            pManager.AddNumberParameter("LensLength", "LensL", "Define the lenslength of the camera", GH_ParamAccess.item, 50.0);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Camera", "C", "Pass the information of animation camera out", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d location = Point3d.Unset;
            Point3d target = Point3d.Unset;
            double lensLength = double.NaN;

            DA.GetData(0, ref location);
            DA.GetData(1, ref target);
            DA.GetData(2, ref lensLength);

            AnimationCamera animationCamera = new AnimationCamera(location, target, lensLength);

            DA.SetData(0, animationCamera);

        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconCamera;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c8b463d7-4603-4c4e-9edc-b5655588a59e"); }
        }
    }
}