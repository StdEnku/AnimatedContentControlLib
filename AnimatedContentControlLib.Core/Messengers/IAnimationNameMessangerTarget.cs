using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedContentControlLib.Core.Messengers;

/// <summary>
/// AnimationNameMessangerで管理するオブジェクト用のインターフェース
/// </summary>
public interface IAnimationNameMessangerTarget
{
    /// <summary>
    /// 自身を特定するためのキー
    /// </summary>
    string? AnimationNameMessangerKey { get; set; }

    /// <summary>
    /// メッセージが届いた際実行されるメソッド
    /// </summary>
    /// <param name="nextAnimationName">送信元からの次のアニメーション名</param>
    void AnimationMessageReceive(string? nextAnimationName);
}
