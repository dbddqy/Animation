using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;

namespace Animation
{
    public class CustomMaterialComponent : GH_Component
    {
        public CustomMaterialComponent()
          : base("Custom Material", "CM",
              "Define custom materials for animation.",
              "Extra", "Animation")
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.tertiary;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("DiffuseColor", "DC", "DiffuseColor", GH_ParamAccess.item, Color.White);
            pManager.AddColourParameter("SpecularColor", "SC", "SpecularColor", GH_ParamAccess.item, Color.White);

            pManager.AddColourParameter("ReflectionColor", "RC", "ReflectionColor", GH_ParamAccess.item, Color.White);
            pManager.AddNumberParameter("Reflectivity", "R", "0.0 is no reflection 1.0 is 100% reflective.", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("ReflectionGlossiness", "RG", "	ReflectionGlossiness", GH_ParamAccess.item, 0.0);
            pManager.AddBooleanParameter("UseFresnelReflection", "UFR", "Define if fresnel reflections are used.", GH_ParamAccess.item, false);
            
            pManager.AddColourParameter("TransparentColor", "TC", "TransparentColor", GH_ParamAccess.item, Color.White);
            pManager.AddNumberParameter("Transparency", "T", "Define the transparency of the material (0.0 = opaque to 1.0 = transparent)", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("IndexOfRefraction", "I", "	Gets or sets the index of refraction of the material, generally >= 1.0 (speed of light in vacuum)/(speed of light in material)", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("RefractionGlossiness", "RG", "RefractionGlossiness", GH_ParamAccess.item, 0.0);

            pManager.AddColourParameter("EmissionColor", "EC", "EmissionColor", GH_ParamAccess.item, Color.Black);
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
            Color diffuseColor = Color.Empty;
            Color specularColor = Color.Empty;

            Color reflectionColor = Color.Empty;
            double reflectivity = double.NaN;
            double reflectionGlossiness = double.NaN;
            bool useFresnelReflection = true;

            Color transparentColor = Color.Empty;
            double transparency = double.NaN;
            double indexOfRefraction = double.NaN;
            double refractionGlossiness = double.NaN;

            Color emissionColor = Color.Empty;

            DA.GetData(0, ref diffuseColor);
            DA.GetData(1, ref specularColor);
            DA.GetData(2, ref reflectionColor);
            DA.GetData(3, ref reflectivity);
            DA.GetData(4, ref reflectionGlossiness);
            DA.GetData(5, ref useFresnelReflection);
            DA.GetData(6, ref transparentColor);
            DA.GetData(7, ref transparency);
            DA.GetData(8, ref indexOfRefraction);
            DA.GetData(9, ref refractionGlossiness);
            DA.GetData(10, ref emissionColor);
            

            var material = new Rhino.DocObjects.Material();
            material.DiffuseColor = diffuseColor;
            material.SpecularColor = specularColor;
            material.ReflectionColor = reflectionColor;
            material.Reflectivity = reflectivity;
            material.ReflectionGlossiness = reflectionGlossiness;
            material.FresnelReflections = useFresnelReflection;
            material.TransparentColor = transparentColor;
            material.Transparency = transparency;
            material.IndexOfRefraction = indexOfRefraction;
            material.RefractionGlossiness = refractionGlossiness;
            material.EmissionColor = emissionColor;

            DA.SetData(0, material);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.iconCustomMaterial;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ff7fd9a6-fc52-4044-8570-c5abf95234dc"); }
        }
    }
}