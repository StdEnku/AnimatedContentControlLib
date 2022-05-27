using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedContentControlLib.Core.Messengers;

public interface IAnimationMessangerTarget
{
    string? AnimationNameMessangerKey { get; set; }

    void AnimationMessageReceive(string? nextAnimationName);
}
