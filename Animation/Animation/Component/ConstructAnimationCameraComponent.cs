using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Animation
{
    public class ConstructAnimationCameraComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the AnimationCameraComponent class.
        /// </summary>
        public ConstructAnimationCameraComponent()
          : base("ConstructAnimationCamera", "CAC",
              "Define a camera for animation",
              "Animation", "AnimationCamera")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Location", "L", "Define the location of the camera", GH_ParamAccess.item, new Point3d(10.0, 10.0, 10.0));
            pManager.AddPointParameter("Target", "T", "Define the target of the camera", GH_ParamAccess.item, Point3d.Origin);
            pManager.AddNumberParameter("LensLength", "LensL", "Define the lenslength of the camera", GH_ParamAccess.item, 50.0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
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
            Point3d location = Point3d.Origin;
            Point3d target = Point3d.Origin;
            double lensLength = double.NaN;

            DA.GetData(0, ref location);
            DA.GetData(1, ref target);
            DA.GetData(2, ref lensLength);

            AnimationCamera animationCamera = new AnimationCamera(location, target, lensLength);

            DA.SetData(0, animationCamera);

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
            get { return new Guid("c8b463d7-4603-4c4e-9edc-b5655588a59e"); }
        }
    }
}