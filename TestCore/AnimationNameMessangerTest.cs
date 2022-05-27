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
    // [���]           : 4�̃^�[�Q�b�g��o�^������ɖ{���\�b�h���s��
    // [�z�肳��錋��] : s_target�ÓI�ϐ��̗v�f����0
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

    // [�e�X�g�Ώ�]     : 
    // [���]           : 
    // [�z�肳��錋��] : 
    [Fact]
    public void RegisterTarget_Test4()
    {
        Assert.True(false);
    }

    // [�e�X�g�Ώ�]     : 
    // [���]           : 
    // [�z�肳��錋��] : 
    [Fact]
    public void RegisterTarget_Test5()
    {
        Assert.True(false);
    }
}