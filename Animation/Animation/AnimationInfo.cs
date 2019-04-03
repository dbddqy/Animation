using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Animation
{
    public class AnimationInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Animation";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconPlugin;
            }
        }
        public override string Description
        {
            get
            {
                return " Plugin for making Animation in Grasshopper";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("372cfddb-79e9-4c69-b942-59e58203df18");
            }
        }

        public override string AuthorName
        {
            get
            {
                return "Yue\nZongzi";
            }
        }
        public override string AuthorContact
        {
            get
            {
                return "dbddqy@gmail.com\nzhongruqing.sun.zongzi@gmail.com";
            }
        }
    }
}
