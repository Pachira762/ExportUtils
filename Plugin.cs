using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PEPlugin;
using PEPlugin.Pmx;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ExportUtils
{
    public class Plugin : IPEPluginOption, IPEPlugin
    {
        IPEPluginHost host;
        Form1 form;

        //
        // IPEPluginOption
        //

        public bool Bootup => false;

        public bool RegisterMenu => true;

        public string RegisterMenuText => "ExportUtils";

        //
        // IPEPlugin
        //

        public string Name => "ExportUtils";

        public string Version => "0.1.0";

        public string Description => "ExportUtils";

        public IPEPluginOption Option => this;

        public void Dispose()
        {
        }

        public void Run(IPERunArgs args)
        {
            host = args.Host;

            if (form == null || form.IsDisposed)
            {
                form = new Form1(this);
            }

            if (!form.Visible)
            {
                form.Show();
            }
        }

        public void RemoveSelectedBones()
        {
            var pmx = host.Connector.Pmx.GetCurrentState();
            if (pmx != null)
            {
                RemoveSelectedBonesInternal(pmx);
            }
        }

        private void RemoveSelectedBonesInternal(IPXPmx pmx)
        {
            var bones = host.Connector.View.PmxView.GetSelectedBoneIndices().Select(i => pmx.Bone[i]).ToList();

            var replaceMap = new Dictionary<IPXBone, IPXBone>();
            foreach (var replaceFrom in bones)
            {
                if (replaceFrom.Parent == null)
                {
                    MessageBox.Show("Cannot delete root bone.");
                    return;
                }

                for (var replaceTo = replaceFrom.Parent; replaceTo != null; replaceTo = replaceTo.Parent)
                {
                    if (!bones.Contains(replaceTo))
                    {
                        replaceMap.Add(replaceFrom, replaceTo);
                        break;
                    }
                }
            }

            foreach (var vertex in pmx.Vertex)
            {
                if (vertex.Bone1 != null && replaceMap.TryGetValue(vertex.Bone1, out var newBone))
                {
                    vertex.Bone1 = newBone;
                }
                if (vertex.Bone2 != null && replaceMap.TryGetValue(vertex.Bone2, out newBone))
                {
                    vertex.Bone2 = newBone;
                }
                if (vertex.Bone3 != null && replaceMap.TryGetValue(vertex.Bone3, out newBone))
                {
                    vertex.Bone3 = newBone;
                }
                if (vertex.Bone4 != null && replaceMap.TryGetValue(vertex.Bone4, out newBone))
                {
                    vertex.Bone4 = newBone;
                }
            }

            foreach (var bone in pmx.Bone)
            {
                if (!replaceMap.ContainsKey(bone))
                {
                    if (bone.Parent != null && replaceMap.TryGetValue(bone.Parent, out var newParent))
                    {
                        bone.Parent = newParent;
                    }

                    if (bone.ToBone != null && replaceMap.ContainsKey(bone.ToBone))
                    {
                        bone.ToOffset = bone.ToBone.Position - bone.Position;
                        bone.ToBone = null;
                    }
                }
            }

            foreach (var bone in bones)
            {
                pmx.Bone.Remove(bone);
            }

            host.Connector.Pmx.Update(pmx);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Vertex);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Bone);
            host.Connector.View.PmxView.UpdateModel_Bone();

            var message = "";
            foreach (var item in replaceMap)
            {
                message += item.Key.Name + " => " + item.Value.Name + "\r\n";
            }
            MessageBox.Show(message);
        }

        public void RemoveUnweightedBones()
        {
            var pmx = host.Connector.Pmx.GetCurrentState();
            if (pmx != null)
            {
                RemoveUnweightedBonesInternal(pmx);
            }
        }

        private void RemoveUnweightedBonesInternal(IPXPmx pmx)
        {
            var referencedBones = new HashSet<IPXBone>();

            // 頂点から参照されているボーン
            foreach (var vertex in pmx.Vertex)
            {
                if (vertex.Bone1 != null)
                {
                    referencedBones.Add(vertex.Bone1);
                }
                if (vertex.Bone2 != null)
                {
                    referencedBones.Add(vertex.Bone2);
                }
                if (vertex.Bone3 != null)
                {
                    referencedBones.Add(vertex.Bone3);
                }
                if (vertex.Bone4 != null)
                {
                    referencedBones.Add(vertex.Bone4);
                }
            }

            // 特殊ボーン
            foreach (var bone in pmx.Bone)
            {
                if (bone.IsIK)
                {
                    referencedBones.Add(bone);
                }

                if (bone.AppendParent != null)
                {
                    referencedBones.Add(bone.AppendParent);
                }
            }

            // 参照されているボーンの祖先ボーン
            foreach (var bone in referencedBones.ToArray())
            {
                for (var parent = bone.Parent; parent != null; parent = parent.Parent)
                {
                    referencedBones.Add(parent);
                }
            }

            var bones = pmx.Bone.ToHashSet();
            bones.ExceptWith(referencedBones);

            foreach(var bone in bones)
            {
                pmx.Bone.Remove(bone);
            }

            host.Connector.Pmx.Update(pmx);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Vertex);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Bone);
            host.Connector.View.PmxView.UpdateModel_Bone();

            var message = "";
            foreach (var bone in bones)
            {
                message += bone.Name + "\r\n";
            }

            if(message.Length == 0)
            {
                message = "No unweighted bone";
            }

            MessageBox.Show(message);
        }

        public void SortMaterialByTexture()
        {
            var pmx = host.Connector.Pmx.GetCurrentState();
            if (pmx != null)
            {
                SortMaterialByTextureInternal(pmx);
            }
        }

        private void SortMaterialByTextureInternal(IPXPmx pmx)
        {
            var materilCount = pmx.Material.Count;
            for (int i = 0; i < materilCount - 1; ++i)
            {
                var material = pmx.Material[i];
                var mat1 = pmx.Material[i + 1];
                if (material.Tex != mat1.Tex)
                {
                    for (int j = i + 2; j < materilCount; ++j)
                    {
                        var mat2 = pmx.Material[j];
                        if (material.Tex == mat2.Tex)
                        {
                            pmx.Material[i + 1] = mat2;
                            pmx.Material[j] = mat1;
                        }
                    }
                }
            }

            host.Connector.Pmx.Update(pmx);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Material);
        }

        public void ReplaceTexturePath(string find, string replace)
        {
            var pmx = host.Connector.Pmx.GetCurrentState();
            if (pmx != null && find.Length > 0)
            {
                ReplaceTexturePathInternal(pmx, find, replace);
            }
        }

        public void ReplaceTexturePathInternal(IPXPmx pmx, string find, string replace)
        {
            try
            {
                foreach (var material in pmx.Material)
                {
                    material.Tex = Regex.Replace(material.Tex, find, replace);
                }

                host.Connector.Pmx.Update(pmx);
                host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Material);
                host.Connector.View.PmxView.UpdateModel();
            }
            catch (Exception e)
            {
            }
        }
    }
}
