using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Forms;
using PEPlugin;
using PEPlugin.Pmx;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace utils
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

        public string RegisterMenuText => "utils";

        //
        // IPEPlugin
        //

        public string Name => "utils";

        public string Version => "0.1.0";

        public string Description => "utils";

        public IPEPluginOption Option => this;

        public void Dispose()
        {
        }

        public void Run(IPERunArgs args)
        {
            host = args.Host;

            if(form == null || form.IsDisposed)
            {
                form = new Form1(this);
            }

            if (!form.Visible)
            {
                form.Show();
            }
        }

        public void RemoveTipBones()
        {
            var pmx = host.Connector.Pmx.GetCurrentState();
            if(pmx != null)
            {
                RemoveTipBonesInternal(pmx);
            }
        }

        private void RemoveTipBonesInternal(IPXPmx pmx)
        {
            var tips = pmx.Bone.Where(bone => bone.Name.EndsWith("先") && !bone.Name.EndsWith("つま先")).ToHashSet();

            var referencedBones = new HashSet<IPXBone>();
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

            foreach (var bone in pmx.Bone)
            {
                if(tips.Contains(bone))
                {
                    continue;
                }

                if (bone.Parent != null)
                {
                    referencedBones.Add(bone.Parent);
                }

                if (bone.AppendParent != null)
                {
                    referencedBones.Add(bone.AppendParent);
                }

                if(bone.IsIK)
                {
                    referencedBones.Add(bone);
                }
            }

            tips.UnionWith(pmx.Bone.Where(bone => !referencedBones.Contains(bone)));
            UnreferenceBones(pmx, tips);

            foreach (var tip in tips)
            {
                pmx.Bone.Remove(tip);
            }

            host.Connector.Pmx.Update(pmx);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Vertex);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Bone);
            host.Connector.View.PmxView.UpdateModel_Bone();

            var message = "";
            foreach(var tip in tips)
            {
                message += tip.Name + "\r\n";
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
            for(int i = 0; i < materilCount - 1; ++i)
            {
                var material = pmx.Material[i];
                var mat1 = pmx.Material[i + 1];
                if(material.Tex != mat1.Tex)
                {
                    for(int j = i + 2; j < materilCount; ++j)
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

        public void RemoveAndReplaceBone()
        {
            var pmx = host.Connector.Pmx.GetCurrentState();
            if (pmx != null)
            {
                RemoveAndReplaceBoneInternal(pmx);
            }
        }

        private void RemoveAndReplaceBoneInternal(IPXPmx pmx)
        {
            var bones = host.Connector.View.PmxView.GetSelectedBoneIndices().Select(i => pmx.Bone[i]).ToList();

            var replaceMap = UnreferenceBones(pmx, bones);
            foreach (var bone in bones)
            {
                pmx.Bone.Remove(bone);
            }

            var message = "";
            foreach (var item in replaceMap)
            {
                message += item.Key.Name + " => " + item.Value.Name + "\r\n";
            }
            MessageBox.Show(message);

            host.Connector.Pmx.Update(pmx);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Vertex);
            host.Connector.Form.UpdateList(PEPlugin.Pmd.UpdateObject.Bone);
            host.Connector.View.PmxView.UpdateModel_Bone();
        }

        private Dictionary<IPXBone, IPXBone> UnreferenceBones(IPXPmx pmx, IEnumerable<IPXBone> bones)
        {
            var replaceMap = new Dictionary<IPXBone, IPXBone>();
            foreach (var replaceFrom in bones)
            {
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
                vertex.Bone1 = (vertex.Bone1 != null && replaceMap.TryGetValue(vertex.Bone1, out var newBone1)) ? newBone1 : vertex.Bone1;
                vertex.Bone2 = (vertex.Bone2 != null && replaceMap.TryGetValue(vertex.Bone2, out var newBone2)) ? newBone2 : vertex.Bone2;
                vertex.Bone3 = (vertex.Bone3 != null && replaceMap.TryGetValue(vertex.Bone3, out var newBone3)) ? newBone3 : vertex.Bone3;
                vertex.Bone4 = (vertex.Bone4 != null && replaceMap.TryGetValue(vertex.Bone4, out var newBone4)) ? newBone4 : vertex.Bone4;
            }

            foreach (var bone in pmx.Bone)
            {
                if (!replaceMap.ContainsKey(bone) && bone.Parent != null && replaceMap.TryGetValue(bone.Parent, out var newParent))
                {
                    bone.Parent = newParent;
                }

                if (!replaceMap.ContainsKey(bone) && bone.ToBone != null && replaceMap.ContainsKey(bone.ToBone))
                {
                    bone.ToOffset = bone.ToBone.Position - bone.Position;
                    bone.ToBone = null;
                }
            }

            return replaceMap;
        }
    }
}
