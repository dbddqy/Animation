using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Animation
{
    public class AnimationInfo : GH_AssemblyInfo
    {
        public override string Name => "Animation";
        public override Bitmap Icon => Properties.Resources.iconPlugin;
        public override string Description => " Plugin for making Animation in Grasshopper";
        public override Guid Id => new Guid("372cfddb-79e9-4c69-b942-59e58203df18");
        public override string AuthorName => "Yue & Zongzi & LinSX";
        public override string AuthorContact => "dbddqy@gmail.com\r\nzhongruqing.sun.zongzi@gmail.com";
    }
}
