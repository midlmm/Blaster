using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Toolitem Config")]
public class ToolitemConfigData : ScriptableObject
{
    public GameObject Prefab => _prefab;
    public float DelayUsing => _delayUsing;
    public AnimatorOverrideController AnimatorOverride => _animatorOverride;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delayUsing;
    [SerializeField] private AnimatorOverrideController _animatorOverride;
}
