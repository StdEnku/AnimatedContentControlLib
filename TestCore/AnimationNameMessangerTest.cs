using Xunit;
using Xunit.Abstractions;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System;
using System.Collections.Generic;
using Messengers = AnimatedContentControlLib.Core.Messengers;
using Moq;

namespace TestCore;

public class AnimationNameMessangerTest
{
    private const string S_TARGETS_LIST_NAME = "s_targets";
    private readonly ITestOutputHelper _testOutputHelper;

    public AnimationNameMessangerTest(ITestOutputHelper testOutputHelper)
    {
        this._testOutputHelper = testOutputHelper;
        Messengers.AnimationNameMessanger.Clear();
    }

    // privateなAnimationNameMessanger.s_targets静的リストを取得
    private static List<WeakReference<Messengers.IAnimationNameMessangerTarget>> GetSTargetStaticList()
    {
        var type = typeof(Messengers.AnimationNameMessanger);
        var info = type.GetField(S_TARGETS_LIST_NAME, BindingFlags.NonPublic | BindingFlags.Static);

        Debug.Assert(info is not null);

        var result = info.GetValue(null) as List<WeakReference<Messengers.IAnimationNameMessangerTarget>>;

        Debug.Assert(result is not null);

        return result;
    }

    // [テスト対象]     : RegisterTarget静的メソッド
    // [状態]           : ダミーのIAnimationNameMessangerTarget型のオブジェクトを引数に渡しメソッド実行後
    // [想定される結果] : s_target静的変数に引数で渡したオブジェクトが追加される。
    [Fact]
    public void RegisterTarget_Test()
    {
        var dummyTargetMock = new Mock<Messengers.IAnimationNameMessangerTarget>();
        Messengers.AnimationNameMessanger.RegisterTarget(dummyTargetMock.Object);

        var s_targets = GetSTargetStaticList();
        var wakeTarget = s_targets.First();

        Messengers.IAnimationNameMessangerTarget? target;

        Debug.Assert(wakeTarget.TryGetTarget(out target));
        Assert.Equal(dummyTargetMock.Object, target);
    }

    // [テスト対象]     : Clear静的メソッド
    // [状態]           : 4個のターゲットを登録した後に本メソッド実行後
    // [想定される結果] : s_target静的変数の要素数が0
    [Fact]
    public void Clear_Test()
    {
        var newTarget1 = new Mock<Messengers.IAnimationNameMessangerTarget>().Object;
        var newTarget2 = new Mock<Messengers.IAnimationNameMessangerTarget>().Object;
        var newTarget3 = new Mock<Messengers.IAnimationNameMessangerTarget>().Object;
        var newTarget4 = new Mock<Messengers.IAnimationNameMessangerTarget>().Object;

        Messengers.AnimationNameMessanger.RegisterTarget(newTarget1);
        Messengers.AnimationNameMessanger.RegisterTarget(newTarget2);
        Messengers.AnimationNameMessanger.RegisterTarget(newTarget3);
        Messengers.AnimationNameMessanger.RegisterTarget(newTarget4);

        var s_targets = GetSTargetStaticList();

        Messengers.AnimationNameMessanger.Clear();

        Assert.Empty(s_targets);
    }

    // [テスト対象]     : 
    // [状態]           : 
    // [想定される結果] : 
    [Fact]
    public void RegisterTarget_Test4()
    {
        Assert.True(false);
    }

    // [テスト対象]     : 
    // [状態]           : 
    // [想定される結果] : 
    [Fact]
    public void RegisterTarget_Test5()
    {
        Assert.True(false);
    }
}