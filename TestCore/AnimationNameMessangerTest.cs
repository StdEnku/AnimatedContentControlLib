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

    // private��AnimationNameMessanger.s_targets�ÓI���X�g���擾
    private static List<WeakReference<Messengers.IAnimationNameMessangerTarget>> GetSTargetStaticList()
    {
        var type = typeof(Messengers.AnimationNameMessanger);
        var info = type.GetField(S_TARGETS_LIST_NAME, BindingFlags.NonPublic | BindingFlags.Static);

        Debug.Assert(info is not null);

        var result = info.GetValue(null) as List<WeakReference<Messengers.IAnimationNameMessangerTarget>>;

        Debug.Assert(result is not null);

        return result;
    }

    // [�e�X�g�Ώ�]     : RegisterTarget�ÓI���\�b�h
    // [���]           : �_�~�[��IAnimationNameMessangerTarget�^�̃I�u�W�F�N�g�������ɓn�����\�b�h���s��
    // [�z�肳��錋��] : s_target�ÓI�ϐ��Ɉ����œn�����I�u�W�F�N�g���ǉ������B
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

    // [�e�X�g�Ώ�]     : Clear�ÓI���\�b�h
    // [���]           : 2�̃^�[�Q�b�g��o�^������ɖ{���\�b�h���s��
    // [�z�肳��錋��] : s_target�ÓI�ϐ��̗v�f����0
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

    // [�e�X�g�Ώ�]     : SetAnimationName�ÓI���\�b�h
    // [���]           : �����̃^�[�Q�b�g��o�^���{���\�b�h���s
    // [�z�肳��錋��] : �����Ŏw�肵���^�[�Q�b�g��AnimationMessageReceive���\�b�h��
    //                    �����Ɏw�肳�ꂽ�����񂪑}�����ꂽ��ԂŌĂ΂��
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

        // ���������ׂ������Ⴂ�ȃL�[�Ń��b�Z�[�W���M
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