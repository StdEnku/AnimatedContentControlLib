using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedContentControlLib.Core.Messengers;

public static class AnimationNameMessanger
{
    private static List<WeakReference<IAnimationMessangerTarget>> s_targets = new();
    private static object s_lock = new();

    public static void RegisterTarget(IAnimationMessangerTarget target)
    {
        var weakTarget = new WeakReference<IAnimationMessangerTarget>(target);

        lock (s_lock)
        {
            s_targets.Add(weakTarget);
        }

        Clean();
    }

    public static void Clean()
    {
        lock (s_lock)
        {
            IAnimationMessangerTarget? currentTarget;

            foreach (var current in s_targets)
            {
                if (!current.TryGetTarget(out currentTarget))
                {
                    s_targets.Remove(current);   
                }
            }
        }
    }

    public static void SetAnimationName(string messangerKey, string? AnimationName)
    {
        lock (s_lock)
        {
            IAnimationMessangerTarget? currentTarget;

            var targets = s_targets.FindAll(currentWeakTarget => 
            {
                if (!currentWeakTarget.TryGetTarget(out currentTarget))
                {
                    return false;
                }

                if (currentTarget.AnimationNameMessangerKey is null)
                {
                    return false;
                }
                
                return currentTarget.AnimationNameMessangerKey == messangerKey;
            });

            foreach (var current in targets)
            {
                if (current.TryGetTarget(out currentTarget))
                {
                    currentTarget.AnimationMessageReceive(AnimationName);
                }
            }
        }
    }
}
