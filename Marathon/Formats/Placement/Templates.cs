using libHSON;
using Marathon.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Marathon.Formats.Placement
{
    public class Templates
    {
        public static List<Actor> ImportProp(string in_path)
        {
            if (!File.Exists(in_path))
                throw new FileNotFoundException("The specified file does not exist.");

            return new PropLibrary(in_path).Actors;
        }

        public static List<Actor> ImportProps(string in_path)
        {
            var result = new List<Actor>();

            foreach (var file in Directory.GetFiles(in_path, "*.prop"))
                result.AddRange(ImportProp(file));

            return result;
        }

        public static List<Actor> ImportJson(string in_path)
        {
            if (!File.Exists(in_path))
                throw new FileNotFoundException("The specified file does not exist.");

            return JsonConvert.DeserializeObject<List<Actor>>(File.ReadAllText(in_path));
        }

        public static void ExportJson(string in_inputPath, string in_outputPath)
        {
            File.WriteAllText(in_outputPath, JsonConvert.SerializeObject(ImportProps(in_inputPath), Formatting.Indented));
        }

        public static void ExportHson(string in_inputPath)
        {
            foreach (var file in Directory.GetFiles(in_inputPath, "*.prop"))
            {
                var prop = new PropLibrary(file);

                foreach (var actor in prop.Actors)
                {
                    var hsonProject = new Project
                    {
                        Metadata = new ProjectMetadata
                        {
                            Name = "Template",
                            Description = $"{actor.Name} Template"
                        }
                    };

                    var hsonObject = new libHSON.Object
                    (
                        name: actor.Name,
                        type: actor.Name,
                        position: Vector3.Zero,
                        rotation: Quaternion.Identity
                    );

                    hsonObject.LocalCustomProperties.Add("startInactive", new libHSON.Parameter(false));
                    hsonObject.LocalCustomProperties.Add("drawDistance", new libHSON.Parameter(0.0));

                    foreach (var param in actor.Parameters)
                    {
                        switch (param.Type)
                        {
                            case StageSetDataType.Boolean:
                                hsonObject.LocalParameters.Add(param.Name, new libHSON.Parameter(false));
                                break;

                            case StageSetDataType.Int32:
                                hsonObject.LocalParameters.Add(param.Name, new libHSON.Parameter((long)0));
                                break;

                            case StageSetDataType.Single:
                                hsonObject.LocalParameters.Add(param.Name, new libHSON.Parameter(0.0));
                                break;

                            case StageSetDataType.String:
                                hsonObject.LocalParameters.Add(param.Name, new libHSON.Parameter(""));
                                break;

                            case StageSetDataType.Vector3:
                                List<libHSON.Parameter> v = [new libHSON.Parameter(0.0), new libHSON.Parameter(0.0), new libHSON.Parameter(0.0)];
                                hsonObject.LocalParameters.Add(param.Name, new libHSON.Parameter(v));
                                break;

                            case StageSetDataType.Object:
                                hsonObject.LocalParameters.Add(param.Name, new libHSON.Parameter($"{{{Guid.Empty}}}"));
                                break;

                            default:
                                throw new ArgumentException($"Invalid parameter type: {param.Type}");
                        }
                    }

                    var dir = Directory.CreateDirectory(FileSystemHelper.GetDirectoryNameOfFileName(file));

                    hsonProject.Objects.Add(hsonObject);
                    hsonProject.Save(Path.Combine(dir.FullName, $"{actor.Name}.hson"), jsonOptions: new() { Indented = true });
                }
            }
        }
    }
}
