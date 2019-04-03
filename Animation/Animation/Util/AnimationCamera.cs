using Rhino;
using Rhino.Geometry;

namespace Animation
{
    public struct AnimationCamera
    {
        private Point3d location;
        private Point3d target;
        private readonly double lensLength;

        public static AnimationCamera Default = new AnimationCamera(new Point3d(10.0, 10.0, 10.0), Point3d.Origin, 50.0);

        public AnimationCamera(Point3d location, Point3d target, double lensLength)
        {
            this.location = location;
            this.target = target;
            this.lensLength = lensLength;
        }

        public void SetAimationCamera()
        {
            var viewPort = RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewport;
            viewPort.SetCameraLocation(this.location, false);
            viewPort.SetCameraDirection(this.target - this.location, true);
            viewPort.Camera35mmLensLength = this.lensLength;
        }
    }
}
