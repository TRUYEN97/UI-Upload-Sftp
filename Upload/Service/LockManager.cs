using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoDownload.Common;
using Org.BouncyCastle.Asn1.Crmf;

namespace Upload.Service
{
    public class LockManager
    {
        private static readonly Lazy<LockManager> _instance = new Lazy<LockManager>(() => new LockManager());
        public enum Reasons 
        {
            LOCK_UPDATE,
            LOCK_INPUT,
            LOCK_LOCATION,
            LOCK_PASSWORD,
            LOCK_CREATE_BT,
            LOCK_LOAD_FILES
        }
        private readonly Dictionary<Control, HashSet<Reasons>> _lockReasons = new Dictionary<Control, HashSet<Reasons>>();
        private readonly Dictionary<Reasons, HashSet<Control>> ReasonGroupControls = new Dictionary<Reasons, HashSet<Control>>();

        private LockManager() { }
        public static LockManager Instance => _instance.Value;
        private void Lock(Control ctrl, Reasons reason)
        {
            if (!_lockReasons.ContainsKey(ctrl))
                _lockReasons[ctrl] = new HashSet<Reasons>();

            _lockReasons[ctrl].Add(reason);
            if (ctrl is TextBoxBase textBox)
            {
                Util.SafeInvoke(textBox, () =>
                {
                    textBox.ReadOnly = true;
                });
            }
            else
            {
                Util.SafeInvoke(ctrl, () =>
                {
                    ctrl.Enabled = false;
                });
            }
        }

        private void Unlock(Control ctrl, Reasons reason)
        {
            if (_lockReasons.ContainsKey(ctrl))
            {
                _lockReasons[ctrl].Remove(reason);

                if (_lockReasons[ctrl].Count == 0)
                {
                    if (ctrl is TextBoxBase textBox)
                    {
                        Util.SafeInvoke(textBox, () =>
                        {
                            textBox.ReadOnly = false;
                        });
                    }
                    else
                    {
                        Util.SafeInvoke(ctrl, () =>
                        {
                            ctrl.Enabled = true;
                        });
                    }
                    _lockReasons.Remove(ctrl);
                }
            }
        }

        public void ForceUnlockAll(Reasons reason)
        {
            foreach (var pair in _lockReasons.ToList())
            {
                Unlock(pair.Key, reason);
            }
        }

        public void ForceLockAll(Reasons reason)
        {
            HashSet<Control> groupElms;
            if (this.ReasonGroupControls.ContainsKey(reason) && (groupElms = this.ReasonGroupControls[reason]) != null)
            {
                foreach (var ctrl in groupElms)
                {
                    Lock(ctrl, reason);
                }
            }
        }

        internal HashSet<Control> GroupControls(Reasons reason)
        {
            if (this.ReasonGroupControls.ContainsKey(reason))
            {
                return this.ReasonGroupControls[reason];
            }
            return null;
        }

        internal void SetLock(bool lockUpdate, Reasons reason)
        {
            if (lockUpdate)
            {
                ForceLockAll(reason);
            }
            else
            {
                ForceUnlockAll(reason);
            }
        }

        internal void AddToGroup(Reasons reason, Control control)
        {
            if (control == null )
            {
                return;
            }
            HashSet<Control> groupElms = GroupControls(reason);
            if (groupElms == null)
            {
                groupElms = new HashSet<Control>();
                this.ReasonGroupControls.Add(reason, groupElms);
            }
            groupElms.Add(control);
        }

        internal void AddToGroup(Reasons reason, ICollection<Control> controls)
        {
            if (controls == null || controls.Count == 0)
            {
                return;
            }
            HashSet<Control> groupElms = GroupControls(reason);
            if (groupElms == null)
            {
                groupElms = new HashSet<Control>();
                this.ReasonGroupControls.Add(reason, groupElms);
            }
            foreach (var control in controls)
            {
                groupElms.Add(control);
            }
        }
    }

}
