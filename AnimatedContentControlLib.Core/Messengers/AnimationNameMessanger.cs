using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedContentControlLib.Core.Messengers;

/// <summary>
/// AnimatedContentControlのCurrentStoryboardKeyプロパティを
/// どこからでも変更できるようにするための
/// メッセンジャーパターンを含む静的クラス
/// 登録したオブジェクトは弱参照で管理されます。
/// </summary>
public static class AnimationNameMessanger
{
    private static List<WeakReference<IAnimationNameMessangerTarget>> s_targets = new();
    private static object s_lock = new();

    /// <summary>
    /// 対象のIAnimationNameMessangerTargetオブジェクトを
    /// メッセンジャーに登録する静的関数
    /// </summary>
    /// <remarks>
    /// 追加処理後にCleanメソッドが実行される。
    /// </remarks>
    /// <param name="target">登録したいオブジェクト</param>
    public static void RegisterTarget(IAnimationNameMessangerTarget target)
    {
        var weakTarget = new WeakReference<IAnimationNameMessangerTarget>(target);

        lock (s_lock)
        {
            s_targets.Add(weakTarget);
        }

        Clean();
    }

    /// <summary>
    /// 登録済みのオブジェクトの参照が切れてる場合
    /// 管理対象リスト(s_targets静的変数)から除外する。
    /// </summary>
    /// <remarks>
    /// このメソッドを実行しても監視対象リストがクリアされる事はありません。
    /// </remarks>
    public static void Clean()
    {
        lock (s_lock)
        {
            // foreach文中で要素数を変えるような操作を行うと
            // 例外が発生するのでディープコピーで対応
            var targets = s_targets.ToArray();
            foreach (var current in targets)
            {
                if (!current.TryGetTarget(out var currentTarget))
                {
                    s_targets.Remove(current);   
                }
            }
        }
    }

    /// <summary>
    /// 指定したAnimatedContentControlの
    /// CurrentStoryboardKeyプロパティを変更する。
    /// </summary>
    /// <param name="aimationNameMessangerKey">対象とするオブジェクトのAimationNameMessangerKeyプロパティ</param>
    /// <param name="AnimationName">変更後のアニメーション名</param>
    public static void SetAnimationName(string aimationNameMessangerKey, string? animationName)
    {
        lock (s_lock)
        {
            var targets = s_targets.FindAll(currentWeakTarget => 
            {
                if (currentWeakTarget.TryGetTarget(out var currentTarget))
                {
                    if (currentTarget is null)
                    {
                        return false;
                    }

                    return currentTarget.AnimationNameMessangerKey == aimationNameMessangerKey;
                }
                else
                {
                    return false;
                }                
            });

            foreach (var currentWeak in targets)
            {
                if (currentWeak.TryGetTarget(out var currentTarget))
                {
                    currentTarget.AnimationMessageReceive(animationName);
                }
            }
        }
    }

    /// <summary>
    /// 管理対象リスト(s_targets静的変数)を全消去する静的関数
    /// </summary>
    public static void Clear()
    {
        lock (s_lock)
        {
            s_targets.Clear();
        }
    }
}
