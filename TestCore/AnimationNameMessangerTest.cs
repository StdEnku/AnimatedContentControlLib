using Xunit;
using Xunit.Abstractions;
using System.Reflection;
using System.Diagnostics;
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
    // [状態]           : 2個のターゲットを登録した後に本メソッド実行後
    // [想定される結果] : s_target静的変数の要素数が0
    [Fact]
    public void Clear_Test()
    {
        var newTarget1 = new Mock<Messengers.IAnimationNameMessangerTarget>().Object;
        var newTarget2 = new Mock<Messengers.IAnimationNameMessangerTarget>().Object;

        Messengers.AnimationNameMessanger.RegisterTarget(newTarget1);
        Messengers.AnimationNameMessanger.RegisterTarget(newTarget2);

        var s_targets = GetSTargetStaticList();

        Messengers.AnimationNameMessanger.Clear();

        Assert.Empty(s_targets);
    }

    // [テスト対象]     : SetAnimationName静的メソッド
    // [状態]           : 複数のターゲットを登録し本メソッド実行
    // [想定される結果] : 引数で指定したターゲットのAnimationMessageReceiveメソッドの
    //                    引数に指定された文字列が挿入された状態で呼ばれる
    [Fact]
    public void SetAnimationName_Test()
    {
        const string KEY1 = "key1";
        const string KEY2 = "key2";
        const string ETC_KEY = "etc_key";
        const string MESSAGE_STR1 = "Message_str1";
        const string MESSAGE_STR2 = "Message_str2";

        int key1CalledNum = 0;
        int key2CalledNum = 0;
        const int EXPECT_CALLED_NUM = 2;

        var newTarget1 = new Mock<Messengers.IAnimationNameMessangerTarget>();
        var newTarget2 = new Mock<Messengers.IAnimationNameMessangerTarget>();
        var newTarget3 = new Mock<Messengers.IAnimationNameMessangerTarget>();
        var newTarget4 = new Mock<Messengers.IAnimationNameMessangerTarget>();

        newTarget1.SetupGet<string>(t => t.AnimationNameMessangerKey).Returns(KEY1);
        newTarget2.SetupGet<string>(t => t.AnimationNameMessangerKey).Returns(KEY2);
        newTarget3.SetupGet<string>(t => t.AnimationNameMessangerKey).Returns(KEY1);
        newTarget4.SetupGet<string>(t => t.AnimationNameMessangerKey).Returns(KEY2);

        newTarget1.Setup(t => t.AnimationMessageReceive(It.IsAny<string>())).Callback(() => key1CalledNum++);
        newTarget2.Setup(t => t.AnimationMessageReceive(It.IsAny<string>())).Callback(() => key2CalledNum++);
        newTarget3.Setup(t => t.AnimationMessageReceive(It.IsAny<string>())).Callback(() => key1CalledNum++);
        newTarget4.Setup(t => t.AnimationMessageReceive(It.IsAny<string>())).Callback(() => key2CalledNum++);

        Messengers.AnimationNameMessanger.RegisterTarget(newTarget1.Object);
        Messengers.AnimationNameMessanger.RegisterTarget(newTarget2.Object);
        Messengers.AnimationNameMessanger.RegisterTarget(newTarget3.Object);
        Messengers.AnimationNameMessanger.RegisterTarget(newTarget4.Object);

        Messengers.AnimationNameMessanger.SetAnimationName(KEY1, MESSAGE_STR1);
        Messengers.AnimationNameMessanger.SetAnimationName(KEY2, MESSAGE_STR2);

        // 無視されるべき見当違いなキーでメッセージ送信
        Messengers.AnimationNameMessanger.SetAnimationName(ETC_KEY, MESSAGE_STR2);
        Messengers.AnimationNameMessanger.SetAnimationName(ETC_KEY, MESSAGE_STR2);
        Messengers.AnimationNameMessanger.SetAnimationName(ETC_KEY, MESSAGE_STR1);
        Messengers.AnimationNameMessanger.SetAnimationName(ETC_KEY, MESSAGE_STR1);

        newTarget1.Verify(o => o.AnimationMessageReceive(MESSAGE_STR1));
        newTarget2.Verify(o => o.AnimationMessageReceive(MESSAGE_STR2));
        newTarget3.Verify(o => o.AnimationMessageReceive(MESSAGE_STR1));
        newTarget4.Verify(o => o.AnimationMessageReceive(MESSAGE_STR2));

        Assert.Equal(key1CalledNum, EXPECT_CALLED_NUM);
        Assert.Equal(key2CalledNum, EXPECT_CALLED_NUM);
    }
}