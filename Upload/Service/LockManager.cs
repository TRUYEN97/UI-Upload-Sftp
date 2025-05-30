using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Upload.Common;

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
            LOCK_LOAD_FILES,
            LOCK_ACCESS_USER_UPDATE
        }
        private readonly Dictionary<object, HashSet<Reasons>> _lockReasons = new Dictionary<object, HashSet<Reasons>>();
        private readonly Dictionary<Reasons, HashSet<object>> ReasonGroupControls = new Dictionary<Reasons, HashSet<object>>();

        private LockManager() { }
        public static LockManager Instance => _instance.Value;
        private void Lock(object obj, Reasons reason)
        {
            if (!_lockReasons.ContainsKey(obj))
                _lockReasons[obj] = new HashSet<Reasons>();

            _lockReasons[obj].Add(reason);
            if (obj is Control control)
            {
                if (control is TextBoxBase textBox)
                {
                    Util.SafeInvoke(textBox, () =>
                    {
                        textBox.ReadOnly = true;
                    });
                }
                else
                {
                    Util.SafeInvoke(control, () =>
                    {
                        control.Enabled = false;
                    });
                }
            }
            else if (obj is LockActionCallBack action)
            {
                action.lockCallBack?.Invoke();
            }
        }

        private void Unlock(object obj, Reasons reason)
        {
            if (_lockReasons.ContainsKey(obj))
            {
                _lockReasons[obj].Remove(reason);

                if (_lockReasons[obj].Count == 0)
                {
                    if (obj is Control control)
                    {
                        if (control is TextBoxBase textBox)
                        {
                            Util.SafeInvoke(textBox, () =>
                            {
                                textBox.ReadOnly = false;
                            });
                        }
                        else
                        {
                            Util.SafeInvoke(control, () =>
                            {
                                control.Enabled = true;
                            });
                        }
                    }
                    else if (obj is LockActionCallBack action)
                    {
                        action.UnlockCallBack?.Invoke();
                    }
                    _lockReasons.Remove(obj);
                }
            }
        }

        private void Add(Reasons reason, object obj)
        {
            if (obj == null)
            {
                return;
            }
            HashSet<object> groupElms = GroupControls(reason);
            if (groupElms == null)
            {
                groupElms = new HashSet<object>();
                this.ReasonGroupControls.Add(reason, groupElms);
            }
            groupElms.Add(obj);
        }
        internal static void ForceUnlockAll(Reasons reason)
        {
            Instance.UnlockAll(reason);
        }

        internal static void ForceLockAll(Reasons reason)
        {
            Instance.LockAll(reason);
        }

        internal void UnlockAll(Reasons reason)
        {
            foreach (var pair in _lockReasons.ToList())
            {
                Unlock(pair.Key, reason);
            }
        }

        internal void LockAll(Reasons reason)
        {
            HashSet<object> groupElms;
            if (this.ReasonGroupControls.ContainsKey(reason) && (groupElms = this.ReasonGroupControls[reason]) != null)
            {
                foreach (var ctrl in groupElms)
                {
                    Lock(ctrl, reason);
                }
            }
        }

        internal static HashSet<object> GetGroupControls(Reasons reason)
        {
            return Instance.GroupControls(reason);
        }

        internal HashSet<object> GroupControls(Reasons reason)
        {
            if (this.ReasonGroupControls.ContainsKey(reason))
            {
                return this.ReasonGroupControls[reason];
            }
            return null;
        }

        internal static void SetLockFor(bool lockUpdate, Reasons reason)
        {
            Instance.SetLock(lockUpdate, reason);
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

        internal static void AddToGroup(Reasons reason, Control control)
        {
            Instance.Add(reason, control);
        }

        internal static void AddToGroup(Reasons reason, LockActionCallBack action)
        {
            Instance.Add(reason, action);
        }

        internal static void AddToGroupFor(Reasons reason, ICollection<object> objs)
        {
            Instance.AddToGroup(reason, objs);
        }

        internal void AddToGroup(Reasons reason, ICollection<object> objs)
        {
            if (objs == null || objs.Count == 0)
            {
                return;
            }
            HashSet<object> groupElms = GroupControls(reason);
            if (groupElms == null)
            {
                groupElms = new HashSet<object>();
                this.ReasonGroupControls.Add(reason, groupElms);
            }
            foreach (var control in objs)
            {
                groupElms.Add(control);
            }
        }
    }

}
