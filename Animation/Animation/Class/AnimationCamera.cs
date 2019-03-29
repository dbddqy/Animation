using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;

namespace Animation
{
    public class AnimationCamera
    {
        public Point3d Location { get; set; }
        public Point3d Target { get; set; }
        public double LensLength { get; set; }

        public static AnimationCamera Default = new AnimationCamera(new Point3d(10.0, 10.0, 10.0), Point3d.Origin, 50.0);

        public AnimationCamera(Point3d location, Point3d target, double lensLength)
        {
            this.Location = location;
            this.Target = target;
            this.LensLength = lensLength;
        }

        public void SetAimationCamera()
        {
            var viewPort = RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewport;
            viewPort.SetCameraLocation(this.Location, false);
            viewPort.SetCameraDirection(this.Target - this.Location, true);
            viewPort.Camera35mmLensLength = this.LensLength;
        }
    }
}
