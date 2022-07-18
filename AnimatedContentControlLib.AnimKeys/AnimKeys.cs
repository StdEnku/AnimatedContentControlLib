namespace AnimatedContentControlLib.BuiltInAnimKeys;

/// <summary>
/// 組み込みアニメーションキー列挙体
/// </summary>
/// <remarks>
/// 本列挙体はWPFアセンブリに依存しない
/// MVVMパターン開発におけるViewModelで
/// 使用する事を想定しているのでAnimatedContentControlとは
/// 別アセンブリに記載している
/// </remarks>
public enum AnimKeys
{
    SlideinToRight, 
    SlideinToLeft,
    SlideinToUp,
    SlideinToDown,

    ModernSlideinToRight,
    ModernSlideinToLeft,
    ModernSlideinToUp,
    ModernSlideinToDown,

    OpacityInOrder,
    OpacityNewContent,
    OpacitySameTime,

    MechanicalRight,
    MechanicalLeft,

    RotateBigToSmoleToBig,
}
